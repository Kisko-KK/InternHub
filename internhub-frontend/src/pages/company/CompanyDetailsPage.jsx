import { Loader, Button, NavigationBar } from "../../components";
import React, { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { CompanyService, LoginService } from "../../services";
import NotFoundPage from "../NotFoundPage";

export default function CompanyDetailsPage() {
  const companyService = new CompanyService();
  const loginService = new LoginService();
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

  async function handleDelete() {
    const userConfirmed = window.confirm("Are you sure you want to delete?");
    if (userConfirmed) {
      const result = await companyService.removeAsync(params.id);
      if (result) {
        if (company.id === loginService.getUserToken().id) {
          loginService.logOut();
          navigate("/login");
        } else {
          window.history.back();
        }
      } else {
        alert("An error occured while deleting... Please try again later!");
      }
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
      <NavigationBar />
      <div className="container">
        <h1 className="text-center">Company Profile</h1>
        <div style={{ height: 20 }}></div>
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
          Address:{" "}
          <b>
            {company.address +
              (company.county ? ", " + company.county.name : "")}
          </b>
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
        <Link className="text-center" to={`/company/edit/${company.id}`}>
          <Button>Edit</Button>
        </Link>
        <Button onClick={handleDelete} buttonColor="danger">
          Delete
        </Button>
      </div>
    </div>
  );
}
