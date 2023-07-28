import React from "react";
import Table from "../Table";
import CompanyComponent from "./CompanyComponent";

export default function CompanyList({ companies, onRemove }) {
  return (
    <Table>
      <thead>
        <tr>
          <td>Name</td>
          <td>Address</td>
          <td>Website</td>
          <td>Actions</td>
        </tr>
      </thead>
      <tbody>
        {companies.map((company) => {
          return (
            <CompanyComponent
              key={company.id}
              company={company}
              onRemove={onRemove}
            />
          );
        })}
      </tbody>
    </Table>
  );
}
