import { Loader, CompanyNavigation } from "../../components";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { CompanyService } from "../../services/CompanyService";

export default function CompanyProfilePage() {
  const companyService = new CompanyService();
  const [loading, setLoading] = useState(true);
  const [company, setCompany] = useState({});
  const params = useParams();

  async function getCompany() {
    await companyService.getByIdAsync(params.id).then((company) => {
      setLoading(false);
      setCompany(company);
    });
  }

  useEffect(() => {
    getCompany();
  }, []);

  if (loading) return <Loader />;
  return (
    <div>
      <CompanyNavigation />
      <h1>Company Profile Page</h1>
      <div>
        <div className="">
          Name: <b>{company.name}</b>
        </div>
        <div className="">
          Address: <b>{company.address}</b>
        </div>
        <div className="">
          Address: <b>{company.county}</b>
        </div>
        <div className="">
          Email: <b>{company.email}</b>
        </div>
        <div className="">
          Phone number: <b>{company.phoneNumber}</b>
        </div>
        <div className="">
          Description: <b>{company.description}</b>
        </div>
        <div className="">
          Website: <b>{company.website}</b>
        </div>
      </div>
    </div>
  );
}
