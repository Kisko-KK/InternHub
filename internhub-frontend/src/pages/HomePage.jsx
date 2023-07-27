import React from "react";
import { LoginService } from "../services";
import { AdminHomePage, CompanyHomePage, StudentHomePage } from "./index";

export default function HomePage() {
  const userToken = new LoginService().getUserToken() ?? { role: "" };
  if (userToken.role.toLowerCase() === "admin") return <AdminHomePage />;
  if (userToken.role.toLowerCase() === "company") return <CompanyHomePage />;
  if (userToken.role.toLowerCase() === "student") return <StudentHomePage />;
  return <div>Home page</div>;
}
