import React from "react";
import Table from "../Table";
import StudentComponent from "./StudentComponent";

export default function StudentsList({ students, onEdit, onRemove }) {
  return (
    <Table>
      <thead>
        <tr>
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
    </Table>
  );
}
