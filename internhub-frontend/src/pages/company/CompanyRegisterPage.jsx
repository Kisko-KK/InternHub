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

export default function CompanyRegisterPage() {
  return (
    <div className="companyRegisterPage">
      <CompanyNavigation />
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
  );
}
