import React from "react";
import { useNavigate } from "react-router-dom";
import { CompanyService } from "../../services";
import Button from "../Button";

export default function CompanyComponent({
  company,
  onRemove,
  isActive = true,
}) {
  const navigate = useNavigate();
  const companyService = new CompanyService();

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
        {isActive && (
          <>
            <Button
              buttonColor="primary"
              onClick={() => navigate(`/company/details/${company.id}`)}
            >
              Details
            </Button>
            <Button
              buttonColor="secondary"
              onClick={() => navigate(`/company/edit/${company.id}`)}
            >
              Edit
            </Button>
            <Button
              buttonColor="danger"
              onClick={async () => {
                const result = window.confirm(
                  "Are you sure that you want to delete this company?"
                );
                if (result) {
                  await companyService.removeAsync(company.id).then(() => {
                    onRemove();
                  });
                }
              }}
            >
              Delete
            </Button>
          </>
        )}
      </td>
    </tr>
  );
}
