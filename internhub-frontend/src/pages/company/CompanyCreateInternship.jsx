import React from "react";
import "../../styles/Company.css";
import { useState, useEffect } from "react";
import { SelectDropdown } from "../../components";
import { Form } from "../../components";
import { StudyAreaService } from "../../services";
import { useNavigate } from "react-router";

export default function CompanyCreateInternship() {
  const [studyAreas, setStudyAreas] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetchStudyAreas();
  }, []);

  const fetchStudyAreas = async () => {
    try {
      const studyAreasData = await new StudyAreaService().getAsync();
      setStudyAreas(studyAreasData);
    } catch (error) {
      console.log("Unable to fetch study areas.", error);
    }
  };
  const handleClick = () => {
    navigate("/company/homepage");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
  };

  return (
    <div className="container">
      <h1 className="text-center mt-5 title">Add new internship</h1>

      <div className="row justify-content-center mt-3">
        <div className="col-md-6">
          <Form onSubmit={handleSubmit}>
            <label htmlFor="nameForm">Name:</label>
            <input type="text" id="nameForm" placeholder="Enter name" />
          </Form>
        </div>
      </div>

      <div className="row justify-content-center mt-3">
        <div className="col-md-6">
          <Form onSubmit={handleSubmit}>
            <label htmlFor="descriptionForm">Description:</label>
            <textarea
              id="descriptionForm"
              rows={5}
              placeholder="Enter description"
            />
          </Form>
        </div>
      </div>

      <div className="row justify-content-center mt-3">
        <div className="col-md-6">
          <Form onSubmit={handleSubmit}>
            <label htmlFor="addressForm">Address:</label>
            <input type="text" id="addressForm" placeholder="Enter address" />
          </Form>
        </div>
      </div>

      <div className="row justify-content-center mt-3">
        <div className="col-md-3">
          <Form onSubmit={handleSubmit}>
            <label htmlFor="startDateForm">Start Date:</label>
            <input type="date" id="startDateForm" className="form-control" />
          </Form>
        </div>

        <div className="col-md-3">
          <Form onSubmit={handleSubmit}>
            <label htmlFor="endDateForm">End Date:</label>
            <input type="date" id="endDateForm" className="form-control" />
          </Form>
        </div>
      </div>

      <div className="row justify-content-center mt-3">
        <div className="col-md-6">
          <Form onSubmit={handleSubmit}>
            <label htmlFor="studyAreaForm">Study Area:</label>
            <SelectDropdown
              placeholder={"Pick study area"}
              name={"studyArea"}
              list={studyAreas}
            />
          </Form>
        </div>
      </div>

      <div className="mt-4">
        <div className="d-flex justify-content-end w-50">
          <button
            type="submit"
            className="btn btn-primary custom-button"
            onClick={handleClick}
          >
            Submit
          </button>
        </div>
      </div>
    </div>
  );
}
