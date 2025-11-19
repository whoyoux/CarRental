import LoginForm from "@/features/login/components/login-form";

export default function Home() {
  return (
    <div className="flex flex-col gap-12">
      <div>
        backend url:{" "}
        {process.env.NEXT_PUBLIC_BACKEND_PATH
          ? process.env.NEXT_PUBLIC_BACKEND_PATH
          : "not set"}
      </div>
      <LoginForm />
    </div>
  );
}
