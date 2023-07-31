import React from "react";
import { CompanyService } from "../../services";
import Button from "../Button";

export default function CompanyAdminComponent({ company, onChange }) {
  const companyService = new CompanyService();

  async function onClick(isAccepted) {
    const result = await companyService.approveAsync(company.id, isAccepted);
    if (result) {
      onChange();
    }
  }
  return (
    <tr>
      <td>{company.name}</td>
      <td>{company.address}</td>
      <td>
        <a href={company.website} target="_blank">
          {company.website}
        </a>
      </td>
      <td>
        <Button buttonColor="success" onClick={async () => await onClick(true)}>
          Accept
        </Button>
        <Button buttonColor="danger" onClick={async () => await onClick(false)}>
          Decline
        </Button>
      </td>
    </tr>
  );
}
