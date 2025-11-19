"use client";

import { User } from "lucide-react";
import Link from "next/link";
import { useAuth } from "@/providers/auth-provider";
import { Button } from "../ui/button";

const Header = () => {
  const { toggleAuth } = useAuth();

  return (
    <header className="mb-4 flex w-full items-center justify-between border-b py-6">
      <Link href="/">
        <h1 className="font-medium">CarRental</h1>
      </Link>
      <div className="flex items-center gap-2">
        <AuthSection />
        <Button onClick={toggleAuth} size="icon" variant="outline">
          <User />
        </Button>
      </div>
    </header>
  );
};

const AuthSection = () => {
  const { isAuthed } = useAuth();
  return <p>{isAuthed ? "true" : "false"}</p>;
};

export default Header;
