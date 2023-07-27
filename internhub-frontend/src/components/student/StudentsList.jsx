import React from "react";
import StudentComponent from "./StudentComponent";

export default function StudentsList({ students, onEdit, onRemove }) {
  return (
    <table className="table table-dark table-striped">
      <thead>
        <tr>
          <td>Id</td>
          <td>First name</td>
          <td>Last name</td>
          <td>Email</td>
          <td>Actions</td>
        </tr>
      </thead>
      <tbody>
        {students.map((student) => {
          return (
            <StudentComponent
              key={student.id}
              student={student}
              onEdit={onEdit}
              onRemove={onRemove}
            />
          );
        })}
      </tbody>
    </table>
  );
}
