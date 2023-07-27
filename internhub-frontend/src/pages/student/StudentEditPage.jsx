import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
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

export default function StudentEditPage() {
  const studentService = new StudentService();
  const countyService = new CountyService();
  const studyAreaService = new StudyAreaService();
  const [loading, setLoading] = useState(true);
  const [student, setStudent] = useState({});
  const [counties, setCounties] = useState([]);
  const [studyArea, setStudyAreas] = useState([]);
  const navigate = useNavigate();
  const params = useParams();

  useEffect(() => {
    async function fetchData() {
      getStudent();
      setCounties(await countyService.getAsync());
      setStudyAreas(await studyAreaService.getAsync());
      setLoading(false);
    }
    fetchData();
  }, []);

  async function getStudent() {
    await studentService.getByIdAsync(params.id).then((student) => {
      setLoading(false);
      setStudent(student);
    });
  }

  if (loading) return <Loader />;

  return (
    <div className="bg-dark">
      <StudentNavigation />
      <h1 className="text-light">Edit student page</h1>
      <Form
        onSubmit={async (e) => {
          console.log(e);
          var newStudent = new Student({
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
          const result = await studentService.updateAsync(
            student.id,
            newStudent
          );
          if (result) {
            navigate("/");
          }
        }}
      >
        <Input name="firstName" text="First name:" value={student.firstName} />
        <Input name="lastName" text="Last name:" value={student.lastName} />
        <Input
          name="phoneNumber"
          text="Phone number:"
          value={student.phoneNumber}
        />
        <SelectDropdown
          text={"Županija:"}
          placeholder={"Odaberite županiju"}
          name={"county"}
          list={counties}
          selectedId={student.countyId}
        />
        <Input name="address" text="Address:" value={student.address} />
        <Input
          name="description"
          text="Description:"
          value={student.description}
        />
        <SelectDropdown
          text={"Područje obrazovanja:"}
          placeholder={"Odaberite područje obrazovanja"}
          name={"studyArea"}
          list={studyArea}
          selectedId={student.studyAreaId}
        />
        <br></br>
        <Button buttonColor="primary" type="submit">
          Save
        </Button>
      </Form>
    </div>
  );
}
