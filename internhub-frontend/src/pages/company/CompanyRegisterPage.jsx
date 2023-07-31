import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Button,
  Form,
  Input,
  Loader,
  SelectDropdown,
  CompanyNavigation,
} from "../../components";
import "../../styles/index.css";
import { CountyService } from "../../services";
import { CompanyService } from "../../services/CompanyService";
import "../../styles/nav.css";
import { Company } from "../../models";

export default function CompanyRegisterPage() {
  const [counties, setCounties] = useState([]);
  const companyService = new CompanyService();
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  useEffect(() => {
    fetchCounties();
  }, []);
  const fetchCounties = async () => {
    try {
      const countiesData = await new CountyService().getAsync();
      setCounties(countiesData);
    } catch (error) {
      console.error("Error fetching data", error);
      setLoading(false);
    }
  };

  return (
    <div className="bg-dark">
      <CompanyNavigation />
      <h1 className>Register company page</h1>
      <Form
        onSubmit={async (e) => {
          e.preventDefault();
          var company = new Company({
            email: e.target.email.value,
            website: e.target.website.value,
            password: e.target.password.value,
            name: e.target.companyName.value,
            firstName: e.target.firstName.value,
            lastName: e.target.lastName.value,
            address: e.target.companyAddress.value,
            phoneNumber: e.target.phoneNumber.value,
            countyId: e.target.county.value,
            description: e.target.description.value,
          });
          console.log(company);
          const result = await companyService.postAsync(company);

          if (result) {
            navigate("/");
          }
        }}
      >
        <Input name="email" text="Email:" />
        <Input type="password" name="password" text="Password:" />
        <Input name="companyName" text="Company name:" />
        <Input name="website" text="Website:" />
        <Input name="firstName" text="First name:" />
        <Input name="lastName" text="Last name:" />
        <Input name="companyAddress" text="Address:" />
        <Input name="phoneNumber" text="Phone number:" />
        <SelectDropdown
          text={"County:"}
          placeholder={"Pick county"}
          name={"county"}
          list={counties}
        />
        <Input name="description" text="Description:" />
        <br />
        <Button type="submit">Create</Button>
      </Form>
    </div>

  );
}
