import React, { useState } from "react";
import { useEffect } from "react";
import { Loader } from "../components";
import { LoginService } from "../services";
import {
  AdminHomePage,
  CompanyHomePage,
  LandingPage,
  StudentHomePage,
} from "./index";

export default function HomePage() {
  const loginService = new LoginService();
  const [loading, setLoading] = useState(true);
  const [role, setRole] = useState("");

  async function getUserRole() {
    let role = await loginService.getUserRoleAsync();
    role = role.toLowerCase();
    setRole(role);
    setLoading(false);
  }

  useEffect(() => {
    getUserRole();
  });

  if (loading) return <Loader />;

  if (role.toLowerCase() === "admin") return <AdminHomePage />;
  if (role.toLowerCase() === "company") return <CompanyHomePage />;
  if (role.toLowerCase() === "student") return <StudentHomePage />;
  return <LandingPage />;
}
