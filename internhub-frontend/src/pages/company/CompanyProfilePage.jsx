import { Loader, CompanyNavigation, Button } from "../../components";
import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";
import { LoginService, CompanyService } from "../../services";
import NotFoundPage from "../NotFoundPage";

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
    setLoading(true);
    getCompany();
  }, [params.id]);
  console.log("params.id:", params.id);
  console.log("Company state:", company);
  if (loading) return <Loader />;
  if (!company) return <NotFoundPage />;
  return (
    <div>
      <CompanyNavigation />
      <h1 className="text-center">Profile</h1>
      <div className="container">
        <div className="">
          Name: <b>{company.name}</b>
        </div>
        <div className="">
          First Name: <b>{company.firstName}</b>
        </div>
        <div className="">
          Last Name: <b>{company.lastName}</b>
        </div>
        <div className="">
          Address: <b>{company.address}</b>
        </div>
        <div className="">
          County: <b>{company.county.name}</b>
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
          Website:{" "}
          <b>
            <a href={company.website} target="_blank">
              {company.website}
            </a>
          </b>
        </div>
        <Link
          className="text-center"
          to={`/company/edit/${new LoginService().getUserToken().id}`}
        >
          <Button>Edit</Button>
        </Link>
      </div>
    </div>
  );
}
