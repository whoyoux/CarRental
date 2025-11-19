"use client";

import {
  createContext,
  type PropsWithChildren,
  useContext,
  useState,
} from "react";

type AuthContextType = {
  isAuthed: boolean;
  toggleAuth: () => void;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: PropsWithChildren) => {
  const [isAuthed, setIsAuthed] = useState(false);

  const toggleAuth = () => {
    setIsAuthed((prev) => !prev);
  };

  return (
    <AuthContext.Provider value={{ isAuthed, toggleAuth }}>
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
