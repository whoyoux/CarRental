"use client";

import {
  createContext,
  type PropsWithChildren,
  useCallback,
  useContext,
  useEffect,
  useRef,
} from "react";
import { toast } from "sonner";
import type { LoginFormValues } from "@/features/login/components/login-form";
import { betterFetch } from "@/lib/better-fetch";
import { useAuthStore } from "@/stores/auth-store";

type AuthContextType = {
  login: (data: LoginFormValues) => Promise<void>;
  logout: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

const REFRESH_INTERVAL = 5 * 60 * 1000;

export const AuthProvider = ({ children }: PropsWithChildren) => {
  const setTokens = useAuthStore((state) => state.setTokens);
  const clearTokens = useAuthStore((state) => state.clearTokens);
  const accessToken = useAuthStore((state) => state.accessToken);
  const refreshToken = useAuthStore((state) => state.refreshToken);
  const userId = useAuthStore((state) => state.userId);
  const intervalRef = useRef<NodeJS.Timeout | null>(null);

  const refreshAccessToken = useCallback(async () => {
    if (!(refreshToken && userId)) {
      return;
    }

    const { data, error } = await betterFetch("@post/Auth/refresh-token", {
      body: {
        userId,
        refreshToken,
      },
    });

    if (error) {
      clearTokens();
      return;
    }

    setTokens(data.accessToken, data.refreshToken, userId);
  }, [refreshToken, userId, setTokens, clearTokens]);

  useEffect(() => {
    if (accessToken && refreshToken && userId) {
      intervalRef.current = setInterval(() => {
        refreshAccessToken();
      }, REFRESH_INTERVAL);
    }

    return () => {
      if (intervalRef.current) {
        clearInterval(intervalRef.current);
      }
    };
  }, [accessToken, refreshToken, userId, refreshAccessToken]);

  const login = async ({ email, password }: LoginFormValues) => {
    const { data, error } = await betterFetch("@post/Auth/login", {
      body: {
        email,
        password,
      },
    });

    if (error) {
      const errorMessage =
        error.status === 400
          ? "Bad email or password"
          : "Something went wrong.";
      toast.error(errorMessage);
      return;
    }

    setTokens(data.accessToken, data.refreshToken, data.id);
    toast.success("Successfully logged in.");
  };

  const logout = () => {
    if (intervalRef.current) {
      clearInterval(intervalRef.current);
    }
    clearTokens();
    toast.success("Successfully logged out.");
  };

  return (
    <AuthContext.Provider value={{ login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth must be used within AuthProvider");
  }
  return context;
};
