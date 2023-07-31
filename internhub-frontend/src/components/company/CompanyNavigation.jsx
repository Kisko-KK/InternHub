import React from "react";
import { Link } from "react-router-dom";
import "../../styles/nav.css";
import { LoginService } from "../../services";
export default function CompanyNavigation() {
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
      </ul>
    </div>
  );
}
