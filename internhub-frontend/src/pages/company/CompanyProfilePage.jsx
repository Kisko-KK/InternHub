import { Loader, CompanyNavigation, Button } from "../../components";
import React, { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { LoginService, CompanyService } from "../../services";
import NotFoundPage from "../NotFoundPage";

export default function CompanyProfilePage() {
  const companyService = new CompanyService();
  const [loading, setLoading] = useState(true);
  const [company, setCompany] = useState({});
  const params = useParams();
  const navigate = useNavigate();

  async function getCompany() {
    await companyService.getByIdAsync(params.id).then((company) => {
      setLoading(false);
      setCompany(company);
    });
  }

  async function handleDelete(event) {
    const userConfirmed = window.confirm("Are you sure you want to delete?");
    if (userConfirmed) {
      try {
        await companyService.removeAsync(params.id);
      } catch (error) {
        console.error("Error deleting company:", error);
      }
    } else {
      navigate(`/company/profile/${new LoginService().getUserToken().id}`);
    }
  }
  useEffect(() => {
    setLoading(true);
    getCompany();
  }, [params.id]);

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
            <a href={company.website} target="_blank" rel="noreferrer">
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
        <Button onClick={handleDelete}>Delete</Button>
      </div>
    </div>
  );
}
