import React, { useState } from "react";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import {
  AdminNavigation,
  Button,
  Paging,
  StudentsList,
} from "../../components";
import { PagedList } from "../../models";
import { StudentService } from "../../services";

export default function AdminHomePage() {
  const studentService = new StudentService();
  const [pagedStudents, setPagedStudents] = useState(new PagedList({}));
  const navigate = useNavigate();

  async function refreshStudents(pageNumber) {
    const data = await studentService.getAsync(pageNumber);
    setPagedStudents(data);
  }

  useEffect(() => {
    refreshStudents(1);
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
        students={pagedStudents.data}
        onEdit={(id) => {
          navigate(`/student/edit/${id}`);
        }}
        onRemove={() => {
          refreshStudents();
        }}
      />
      <Paging
        currentPage={pagedStudents.currentPage}
        lastPage={pagedStudents.lastPage}
        onPageChanged={(page) => {
          refreshStudents(page);
          // console.log(page);
          // setCurrentPage(page);
        }}
      />
    </div>
  );
}
