import { create } from "zustand";
import { persist } from "zustand/middleware";

type AuthTokens = {
  accessToken: string | null;
  refreshToken: string | null;
  userId: string | null;
};

type AuthStore = AuthTokens & {
  setTokens: (
    accessToken: string,
    refreshToken: string,
    userId: string
  ) => void;
  clearTokens: () => void;
  isAuthenticated: () => boolean;
};

export const useAuthStore = create<AuthStore>()(
  persist(
    (set, get) => ({
      accessToken: null,
      refreshToken: null,
      userId: null,
      setTokens: (accessToken, refreshToken, userId) => {
        set({ accessToken, refreshToken, userId });
      },
      clearTokens: () => {
        set({ accessToken: null, refreshToken: null, userId: null });
      },
      isAuthenticated: () => get().accessToken !== null,
    }),
    {
      name: "auth-storage",
    }
  )
);
