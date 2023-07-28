import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  AdminNavigation,
  Button,
  Paging,
  StudentFilterComponent,
  StudentsList,
} from "../../components";
import { PagedList } from "../../models";
import { StudentService } from "../../services";

export default function AdminStudentsPage() {
  const studentService = new StudentService();
  const [pagedStudents, setPagedStudents] = useState(new PagedList({}));
  const [currentFilter, setCurrentFilter] = useState({});
  const navigate = useNavigate();

  async function refreshStudents({ pageNumber, ...filter }) {
    const data = await studentService.getAdminAsync({ pageNumber, ...filter });
    setPagedStudents(data);
  }

  useEffect(() => {
    refreshStudents(1);
  }, []);

  return (
    <div className="bg-dark">
      <AdminNavigation />
      <h1 className="text-light">Students</h1>
      <StudentFilterComponent
        onFilter={(filter) => {
          setCurrentFilter(filter);
          refreshStudents({ pageNumber: 1, ...filter });
        }}
        onClearFilter={() => refreshStudents({ pageNumber: 1 })}
      />
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
          refreshStudents({ pageNumber: 1, ...currentFilter });
        }}
      />
      <Paging
        currentPage={pagedStudents.currentPage}
        lastPage={pagedStudents.lastPage}
        onPageChanged={(page) => {
          refreshStudents({ pageNumber: page });
        }}
      />
    </div>
  );
}
