import React, { useEffect, useState } from "react";
import {
  Internship,
  Paging,
  InternshipFilter,
  StudentNavigation,
} from "../../components";
import "../../styles/student.css";
import { PagedList } from "../../models";
import { InternshipService } from "../../services";
import { useNavigate } from "react-router-dom";

export default function StudentHomePage() {
  const [pagedInternships, setPagedInternships] = useState(new PagedList({}));
  const [filterData, setFilterData] = useState({});
  const internshipService = new InternshipService();

  const navigate = useNavigate();

  const refreshInternships = async (currentPage, filterData) => {
    const data = await internshipService.getAsync(currentPage, filterData);
    setFilterData(filterData);
    setPagedInternships(data);
  };

  useEffect(() => {
    refreshInternships(1, filterData);
  }, []);

  return (
    <div className="container">
      <StudentNavigation />
      <div>Student Home page</div>
      <InternshipFilter
        onFilter={(filter) => refreshInternships(1, filter)}
        onCancel={() => {
          refreshInternships(1, {});
        }}
      ></InternshipFilter>
      {pagedInternships.data.map((internship) => (
        <Internship
          key={internship.id}
          internship={internship}
          buttonText={"Details"}
          hasApplicationsCount={false}
          redirectTo={() => {
            navigate("/login");
          }}
        />
      ))}
      <Paging
        currentPage={pagedInternships.currentPage}
        lastPage={pagedInternships.lastPage}
        onPageChanged={(page) => {
          refreshInternships(page, filterData);
        }}
      />
    </div>
  );
}
