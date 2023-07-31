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
import { CountyService } from "../../services";
import '../../styles/nav.css'

export default function CompanyRegisterPage() {
  const[counties, setCounties] = useState([]);
  const navigate = useNavigate();

  useEffect (() =>{
    fetchCounties()
  },[])

  const fetchCounties = async () =>{
  try{
    const countiesData = await new CountyService().getAsync();
    setCounties(countiesData);
     }
  catch(error){
    console.error("Error fetching data", error);
  }
}
  return (
    <div> 
      <CompanyNavigation/>
    <div className="companyRegisterPage">
      <h1 className>Register company page</h1>
      <Form>
        <Input name="companyName" text="Company name:" />
        <Input name="companyAddress" text="Address:" />
        <SelectDropdown
          text={"County:"}
          placeholder={"Pick county"}
          name={"county"}
          list={counties}
        />
      </Form>
    </div>
    </div>
  );
}
