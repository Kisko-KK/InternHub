import React, { useState } from "react";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { AdminNavigation, Button, StudentsList } from "../../components";
import { StudentService } from "../../services";

export default function AdminHomePage() {
  const studentService = new StudentService();
  const [students, setStudents] = useState([]);
  const navigate = useNavigate();

  async function refreshStudents() {
    const data = await studentService.getAsync();
    setStudents(data);
  }

  useEffect(() => {
    refreshStudents();
  }, []);

  return (
    <div className="bg-dark">
      <AdminNavigation />
      <h1 className="text-light">HomePage</h1>
      <Button
        buttonColor="success"
        onClick={() => {
          navigate("/student/register");
        }}
      >
        New student
      </Button>
      <StudentsList
        students={students}
        onEdit={(id) => {
          navigate(`/student/edit/${id}`);
        }}
        onRemove={() => {
          refreshStudents();
        }}
      />
    </div>
  );
}
