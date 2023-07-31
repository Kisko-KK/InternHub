import React, { useEffect, useState } from "react";
import {
  Internship,
  Paging,
  InternshipFilter,
  NavigationBar,
} from "../../components";
import "../../styles/student.css";
import { PagedList } from "../../models";
import { InternshipService } from "../../services";
import { useNavigate } from "react-router-dom";
import { useSearchParams } from "react-router-dom/dist";

export default function StudentHomePage() {
  const [pagedInternships, setPagedInternships] = useState(new PagedList({}));
  const [searchParams, setSearchParams] = useSearchParams();
  const [filterData, setFilterData] = useState({
    pageNumber: +(searchParams.get("pageNumber") ?? 1),
    name: searchParams.get("name"),
    endDate: searchParams.get("endDate"),
    startDate: searchParams.get("startDate"),
    counties: searchParams.getAll("counties"),
  });
  const internshipService = new InternshipService();

  const navigate = useNavigate();

  const refreshInternships = async () => {
    const data = await internshipService.getAsync({
      ...filterData,
      sortBy: "Id",
      sortOrder: "ASC",
      pageSize: 10,
    });
    setPagedInternships(data);
  };

  useEffect(() => {
    refreshInternships();
  }, [searchParams, filterData]);

  return (
    <div className="container">
      <NavigationBar />
      <div>Student Home page</div>
      <InternshipFilter
        filter={filterData}
        onFilter={(filter) => {
          setSearchParams({ ...filter, pageNumber: 1 });
          setFilterData({ ...filter, pageNumber: 1 });
        }}
        onClearFilter={() => {
          setSearchParams({ pageNumber: 1 });
          setFilterData({ pageNumber: 1 });
        }}
      />
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
          setSearchParams({ ...filterData, pageNumber: page });
          setFilterData({ ...filterData, pageNumber: page });
        }}
      />
    </div>
  );
}
