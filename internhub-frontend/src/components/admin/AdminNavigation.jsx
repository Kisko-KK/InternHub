import React from "react";
import { Link, useNavigate } from "react-router-dom";
import Button from "../Button";
import "../../styles/nav.css";
import { LoginService } from "../../services";

export default function AdminNavigation() {
  const navigate = useNavigate();
  return (
    <div className="Nav">
      <div className="d-flex justify-content-center align-items-center">
        <ul className="nav-links">
          <Link className="link-style" to="/">
            <li className="nav-item">Home</li>
          </Link>
          <Link className="link-style" to="/students">
            <li className="nav-item">Students</li>
          </Link>
          <Link className="link-style" to="/companies">
            <li className="nav-item">Companies</li>
          </Link>
          {/* <Link className="link-style" to="">
          <li className="nav-item">Internships</li>
        </Link> */}
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
    </div>
  );
}
