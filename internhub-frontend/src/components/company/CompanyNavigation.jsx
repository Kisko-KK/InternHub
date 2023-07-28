import React from "react";
import { Link } from "react-router-dom";

export default function CompanyNavigation() {
  return (
    <div className="Nav">
      <ul className="nav-links">
        <Link className="link-style" to="/">
          <li className="nav-item">Home</li> //Company Home Page
        </Link>
        <Link className="link-style" to="">
          <li className="nav-item">Internship applications</li> //Internship
          Application
        </Link>
      </ul>
    </div>
  );
}
