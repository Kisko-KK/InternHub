import React from "react";
import { Link } from "react-router-dom";
import "../../styles/nav.css";
import { LoginService } from "../../services";
export default function StudentNavigation() {
  return (
    <div className="Nav">
      <ul className="nav-links">
        <Link className="link-style" to="/">
          <li className="nav-item">Home</li>
        </Link>
        <Link className="link-style" to="">
          <li className="nav-item">My internships</li>
        </Link>
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
