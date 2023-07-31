import React from "react";
import { Link, useNavigate } from "react-router-dom";
import Button from "../Button";
import "../../styles/nav.css";
import { LoginService } from "../../services";

export default function StudentNavigation() {
  const navigate = useNavigate();
  return (
    <div className="Nav">
      <ul className="nav-links">
        <Link className="link-style" to="/">
          <li className="nav-item">Home</li>
        </Link>
        <Link className="link-style" to="">
          <li className="nav-item">My internships</li>
        </Link>
        <Button
          onClick={() => {
            const loginService = new LoginService();
            if (loginService.logOut()) navigate("/login");
          }}
        >
          Logout
        </Button>
        <Link
          className="link-style"
          to={`/student/details/${new LoginService().getUserToken().id}`}
        >
          <li className="nav-item">My profile</li>
        </Link>
      </ul>
    </div>
  );
}
