"use client";

import Link from "next/link";
import { ThemeDropdown } from "../misc/theme-dropdown";
import { useAuth } from "@/providers/auth-provider";
import { useAuthStore } from "@/stores/auth-store";
import { Button } from "../ui/button";

const Header = () => {
  const { logout } = useAuth();
  const isAuthenticated = useAuthStore((state) => state.accessToken);

  return (
    <header className="mb-6 flex w-full items-center justify-between border-b py-6">
      <Link href="/">
        <h1 className="font-medium">CarRental</h1>
      </Link>
      <div className="flex items-center gap-2">
        {isAuthenticated && (
          <Button onClick={logout} variant="outline">
            Wyloguj
          </Button>
        )}
        <ThemeDropdown />
      </div>
    </header>
  );
};

export default Header;
