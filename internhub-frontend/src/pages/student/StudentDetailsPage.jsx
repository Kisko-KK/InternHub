import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Loader, StudentNavigation } from "../../components";
import { StudentService } from "../../services";

export default function StudentDetailsPage() {
  const studentService = new StudentService();
  const [loading, setLoading] = useState(true);
  const [student, setStudent] = useState({});
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

  return (
    <div className="bg-dark">
      <StudentNavigation />
      <h1 className="text-light">Student Details Page</h1>
      <div>
        <div className="mb-3 mt-3 w-50 text-light">
          First Name: <b>{student.firstName}</b>
        </div>
        <div className="mb-3 mt-3 w-50 text-light">
          Last Name: <b>{student.lastName}</b>
        </div>
        <div className="mb-3 mt-3 w-50 text-light">
          Email: <b>{student.email}</b>
        </div>
        <div className="mb-3 mt-3 w-50 text-light">
          Phone number: <b>{student.phoneNumber}</b>
        </div>
        <div className="mb-3 mt-3 w-50 text-light">
          Address: <b>{student.address}</b>
        </div>
        <div className="mb-3 mt-3 w-50 text-light">
          Description: <b>{student.description}</b>
        </div>
      </div>
    </div>
  );
}
