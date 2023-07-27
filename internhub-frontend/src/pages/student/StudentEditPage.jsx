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
        <div className="mb-3 mt-3 w-50">
          <Input
            name="firstName"
            text="First name:"
            value={student.firstName}
          />
        </div>

        <div className="mb-3 mt-3 w-50">
          <Input name="lastName" text="Last name:" value={student.lastName} />
        </div>
        <div className="mb-3 mt-3 w-50">
          <Input
            name="phoneNumber"
            text="Phone number:"
            value={student.phoneNumber}
          />
        </div>
        <div className="mb-3 mt-3 w-50">
          <SelectDropdown
            text={"Županija:"}
            placeholder={"Odaberite županiju"}
            name={"county"}
            list={counties}
          />
        </div>
        <div className="mb-3 mt-3 w-50">
          <Input name="address" text="Address:" value={student.address} />
        </div>

        <div className="mb-3 mt-3 w-50">
          <Input
            name="description"
            text="Description:"
            value={student.description}
          />
          <div className="mb-3 mt-3 w-50">
            <SelectDropdown
              text={"Područje obrazovanja:"}
              placeholder={"Odaberite područje obrazovanja"}
              name={"studyArea"}
              list={studyArea}
            />
          </div>
        </div>
        <br></br>
        <Button buttonColor="primary" type="submit">
          Save
        </Button>
      </Form>
    </div>
  );
}
