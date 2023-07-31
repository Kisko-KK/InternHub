import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Loader, NavigationBar } from "../../components";
import { StudentService } from "../../services";
import NotFoundPage from "../NotFoundPage";

export default function StudentDetailsPage() {
  const studentService = new StudentService();
  const [loading, setLoading] = useState(true);
  const [student, setStudent] = useState(null);
  const params = useParams();

  async function getStudent() {
    await studentService.getByIdAsync(params.id).then((student) => {
      setLoading(false);
      setStudent(student);
    });
  }

  useEffect(() => {
    getStudent();
  }, []);

  if (loading) return <Loader />;
  if (!student) return <NotFoundPage />;

  return (
    <div>
      <NavigationBar />
      <div className="container">
        <div className="text-center">
          <h1>Student Details Page</h1>
        </div>
        <div className="row mb-3 mt-">
          First Name: <b>{student.firstName}</b>
        </div>
        <div className="row mb-3 mt-3">
          Last Name: <b>{student.lastName}</b>
        </div>
        <div className="row mb-3 mt-">
          Email: <b>{student.email}</b>
        </div>
        <div className="row mb-3 mt-">
          Phone number: <b>{student.phoneNumber}</b>
        </div>
        <div className="row mb-3 mt-3">
          Address: <b>{student.address}</b>
        </div>
        <div className="row mb-3 mt-3">
          Description: <b>{student.description}</b>
        </div>
      </div>
    </div>
  );
}
