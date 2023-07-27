import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  Button,
  Form,
  Input,
  Loader,
  SelectDropdown,
  StudentNavigation,
} from "../../components";
import { Student } from "../../models";
import {
  CountyService,
  StudentService,
  StudyAreaService,
} from "../../services";

export default function StudentRegisterPage() {
  const countyService = new CountyService();
  const studyAreaService = new StudyAreaService();
  const studentService = new StudentService();
  const [loading, setLoading] = useState(true);
  const [counties, setCounties] = useState([]);
  const [studyArea, setStudyAreas] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    async function fetchData() {
      setCounties(await countyService.getAsync());
      setStudyAreas(await studyAreaService.getAsync());
      setLoading(false);
    }
    fetchData();
  }, []);

  if (loading) return <Loader />;
  return (
    <div className="bg-dark">
      <StudentNavigation />
      <h1 className="text-light">Edit student page</h1>
      <Form
        onSubmit={async (e) => {
          e.preventDefault();
          console.log(e);
          var student = new Student({
            firstName: e.target.firstName.value,
            lastName: e.target.lastName.value,
            email: e.target.email.value,
            phoneNumber: e.target.phoneNumber.value,
            address: e.target.address.value,
            description: e.target.description.value,
            password: e.target.password ? e.target.password.value : null,
            countyId: e.target.county.value,
            studyAreaId: e.target.studyarea.value,
          });
          const result = await studentService.postAsync(student);
          if (result) {
            navigate("/");
          }
        }}
      >
        <Input name="firstName" text="First name:" />
        <Input name="lastName" text="Last name:" />
        <Input name="phoneNumber" text="Phone number:" />
        <SelectDropdown
          text={"Županija:"}
          placeholder={"Odaberite županiju"}
          name={"county"}
          list={counties}
        />
        <Input name="address" text="Address:" />
        <Input name="description" text="Description:" />
        <SelectDropdown
          text={"Područje obrazovanja:"}
          placeholder={"Odaberite područje obrazovanja"}
          name={"studyArea"}
          list={studyArea}
        />
        <Input name="email" text="Email:" />
        <Input type="password" name="password" text="Password:" />
        <br></br>
        <Button buttonColor="primary" type="submit">
          Create
        </Button>
      </Form>
    </div>
  );
}
