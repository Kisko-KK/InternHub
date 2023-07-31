import React, { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate, useParams } from "react-router-dom";
import {
  Form,
  Input,
  Loader,
  SelectDropdown,
  CompanyNavigation,
  Button,
} from "../../components";
import { CountyService, CompanyService } from "../../services";
import { HttpHeader } from "../../models";

export default function CompanyEditPage() {
  const [counties, setCounties] = useState([]);
  const navigate = useNavigate();
  const [loading, setLoading] = useState(true);
  const companyService = new CompanyService();
  const countyService = new CountyService();
  const params = useParams();

  const [company, setCompany] = useState({
    email: "",
    password: "",
    companyName: "",
    website: "",
    firstName: "",
    lastName: "",
    companyAddress: "",
    phoneNumber: "",
    countyId: "",
    description: "",
  });

  useEffect(() => {
    setLoading(true);
    getCompany();
    fetchCounties();
  }, []);

  async function getCompany() {
    try {
      const companyData = await companyService.getByIdAsync(params.id);
      setCompany(companyData);
      setLoading(false);
    } catch (error) {
      console.error("Error fetching company data", error);
      setLoading(false);
    }
  }

  const fetchCounties = async () => {
    try {
      const countiesData = await countyService.getAsync();
      setCounties(countiesData);
      setLoading(false);
    } catch (error) {
      console.log("Error fetching data:", error);
      setLoading(false);
    }
  };

  async function handleFormSubmit(event) {
    event.preventDefault();
    setLoading(true);
    try {
      // Call the updateAsync function to update the company data
      const success = await updateAsync(params.id, company);
      if (success) {
        // Redirect to the company details page after successful update
        navigate(`/company/profile/${params.id}`);
      } else {
        console.error("Failed to update company data.");
      }
      setLoading(false);
    } catch (error) {
      console.error("Error updating company data", error);
      setLoading(false);
    }
  }

  async function updateAsync(id, company) {
    try {
      // Assuming that `axios` is imported and configured appropriately
      const response = await axios.put(
        "https://localhost:44332/api/Company/" + id,
        company,
        {
          headers: HttpHeader.get(),
        }
      );
      return response.status === 200;
    } catch {
      return false;
    }
  }

  if (loading) return <Loader />;
  return (
    <div>
      <CompanyNavigation />
      <h1>Edit Company</h1>
      <Form onSubmit={handleFormSubmit}>
        <Input
          name="email"
          text="Email:"
          value={company.email}
          onChange={(e) => setCompany({ ...company, email: e.target.value })}
        />
        <Input
          type="password"
          name="password"
          text="Password:"
          value={company.password}
          onChange={(e) => setCompany({ ...company, password: e.target.value })}
        />
        <Input
          name="companyName"
          text="Company name:"
          value={company.companyName}
          onChange={(e) =>
            setCompany({ ...company, companyName: e.target.value })
          }
        />
        <Input
          name="website"
          text="Website:"
          value={company.website}
          onChange={(e) => setCompany({ ...company, website: e.target.value })}
        />
        <Input
          name="firstName"
          text="First name:"
          value={company.firstName}
          onChange={(e) =>
            setCompany({ ...company, firstName: e.target.value })
          }
        />
        <Input
          name="lastName"
          text="Last name:"
          value={company.lastName}
          onChange={(e) => setCompany({ ...company, lastName: e.target.value })}
        />
        <Input
          name="companyAddress"
          text="Address:"
          value={company.companyAddress}
          onChange={(e) =>
            setCompany({ ...company, companyAddress: e.target.value })
          }
        />
        <Input
          name="phoneNumber"
          text="Phone number:"
          value={company.phoneNumber}
          onChange={(e) =>
            setCompany({ ...company, phoneNumber: e.target.value })
          }
        />
        <SelectDropdown
          text={"County:"}
          placeholder={"Pick county"}
          name={"county"}
          list={counties}
          value={company.countyId}
          onChange={(e) => setCompany({ ...company, countyId: e.target.value })}
        />
        <Input
          name="description"
          text="Description:"
          value={company.description}
          onChange={(e) =>
            setCompany({ ...company, description: e.target.value })
          }
        />
        <br />
        <Button type="submit">Save</Button>
      </Form>
    </div>
  );
}
