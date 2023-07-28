import React from "react";
import { useNavigate } from "react-router-dom";
import { StudentService } from "../../services";
import Button from "../Button";

export default function StudentComponent({ student, onEdit, onRemove }) {
  const studentService = new StudentService();
  const navigate = useNavigate();
  return (
    <tr>
      <td>{student.firstName}</td>
      <td>{student.lastName}</td>
      <td>{student.email}</td>
      <td>
        <Button
          buttonColor="primary"
          onClick={() => navigate(`/student/details/${student.id}`)}
        >
          Details
        </Button>
        <Button buttonColor="secondary" onClick={() => onEdit(student.id)}>
          Edit
        </Button>
        <Button
          buttonColor="danger"
          onClick={async () => {
            const result = window.confirm(
              "Jeste li sigurni da zelite ukloniti ovog korisnika?"
            );
            if (result) {
              await studentService.removeAsync(student.id).then(() => {
                onRemove();
              });
            }
          }}
        >
          Delete
        </Button>
      </td>
    </tr>
  );
}
