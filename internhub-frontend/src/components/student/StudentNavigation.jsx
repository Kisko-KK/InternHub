import React from "react";
import { Link } from "react-router-dom";

export default function StudentNavigation() {
  return (
    <div className="Nav">
      <ul className="nav-links">
        <Link className="link-style" to="/">
          <li className="nav-item">Home</li> //Student Home Page
        </Link>
        <Link className="link-style" to="">
          <li className="nav-item">My internships</li>
        </Link>
      </ul>
    </div>
  );
}
