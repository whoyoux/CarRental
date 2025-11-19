import { createFetch } from "@better-fetch/fetch";

if (!process.env.NEXT_PUBLIC_BACKEND_PATH) {
  throw new Error("BACKEND_PATH does not exists in .env!");
}

export const betterFetch = createFetch({
  baseURL: `${process.env.NEXT_PUBLIC_BACKEND_PATH}/api`,
});
