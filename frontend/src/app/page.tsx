"use client";

import LoginForm from "@/features/login/components/login-form";
import { useAuthStore } from "@/stores/auth-store";

export default function Home() {
  const isAuthenticated = useAuthStore((state) => state.accessToken);

  return (
    <div className="flex flex-col gap-12">
      <div>
        backend url:{" "}
        {process.env.NEXT_PUBLIC_BACKEND_PATH
          ? process.env.NEXT_PUBLIC_BACKEND_PATH
          : "not set"}
      </div>
      {isAuthenticated ? "you are logged in!" : <LoginForm />}
    </div>
  );
}
