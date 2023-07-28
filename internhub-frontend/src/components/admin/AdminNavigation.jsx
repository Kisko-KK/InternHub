import React from "react";
import { Link } from "react-router-dom";
import "../../styles/nav.css";
export default function AdminNavigation() {
  return (
    <div className="Nav">
      <ul className="nav-links">
        <Link className="link-style" to="/">
          <li className="nav-item">Home</li>
        </Link>
        <Link className="link-style" to="">
          <li className="nav-item">Students</li>
        </Link>
        <Link className="link-style" to="">
          <li className="nav-item">Companies</li>
        </Link>
        <Link className="link-style" to="">
          <li className="nav-item">Internships</li>
        </Link>
      </ul>
    </div>
  );
}
