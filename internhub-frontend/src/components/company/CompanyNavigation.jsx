import React from "react";
import { Link, useNavigate } from "react-router-dom";
import Button from "../Button";
import { LoginService } from "../../services";

export default function CompanyNavigation() {
  const navigate = useNavigate();
  return (
    <div className="Nav">
      <ul className="nav-links">
        <Link className="link-style" to="/company">
          <li className="nav-item">Home</li>
        </Link>
        <Link className="link-style" to="">
          <li className="nav-item">Internship applications</li>
        </Link>
        <Link
          className="link-style"
          to={`/company/profile/${new LoginService().getUserToken().id}`}
        >
          <li className="nav-item">Profile</li>
        </Link>
        <Button
          onClick={() => {
            const loginService = new LoginService();
            if (loginService.logOut()) navigate("/login");
          }}
        >
          Logout
        </Button>
      </ul>
    </div>
  );
}
