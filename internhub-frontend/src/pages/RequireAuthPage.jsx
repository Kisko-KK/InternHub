import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { LoginService } from "../services";
import { NotFoundPage } from "../pages";

export default function RequireAuthPage({ roles, page }) {
  const navigate = useNavigate();
  const loginService = new LoginService();
  const userRole = loginService.getUserRole();

  useEffect(() => {
    if (!loginService.isUserTokenValid()) {
      navigate("/login");
    }
  }, []);

  if (
    !roles ||
    roles.length === 0 ||
    roles.map((role) => role.toLowerCase()).includes(userRole.toLowerCase())
  ) {
    return page;
  }

  return <NotFoundPage />;
}
