import React from "react";
import { Button } from "react-bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import "../styles/landingPage.css";
import { useNavigate } from "react-router-dom";

const LandingPage = () => {
  const navigate = useNavigate();

  const handleStudentRegistry = () => {
    navigate("/student/register");
  };

  const handleCompanyRegistry = () => {
    navigate("/company/register");
  };

  const handleLogin = () => {
    navigate("/login");
  };
  return (
    <div className="d-flex flex-column align-items-center mt-4 justify-content-around">
      <div className="d-flex align-items-center">
        <img src="/logo192.png" alt="Logo" width="80" height="80" />
        <h2 className="title ml-3">Welcome to InternHub</h2>
      </div>
      <div className="mt-5" />
      <img src="/images/hello.svg" alt="hello" width={600} />
      <div className="d-flex justify-content-between w-50 mt-5">
        <Button
          variant="primary"
          className="custom-button"
          onClick={handleStudentRegistry}
        >
          Register as a student
        </Button>
        <Button
          variant="primary"
          className="custom-button"
          onClick={handleCompanyRegistry}
        >
          Register as a company
        </Button>
      </div>
      <Button
        variant="primary"
        className="custom-button mt-5"
        onClick={handleLogin}
      >
        Login
      </Button>
    </div>
  );
};

export default LandingPage;
