import React from "react";
import { Internship } from "../../components";
import "../../styles/student.css"

export default function StudentHomePage() {
  return (
    <div className="container">
      <div>Student Home page</div>
      <Internship buttonText={"Details"}/>
      <Internship buttonText={"Details"}/>
      <Internship buttonText={"Details"}/>
      <Internship buttonText={"Details"}/>
      <Internship buttonText={"Details"}/>
    </div>
  );
  
}
