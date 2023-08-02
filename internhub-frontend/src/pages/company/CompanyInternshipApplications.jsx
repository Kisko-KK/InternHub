import React, { useState } from "react";
import { useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import {
  InternshipApplicationComponent,
  InternshipApplicationFilter,
  NavigationBar,
  NoItems,
  Paging,
} from "../../components";
import { PagedList } from "../../models";
import { InternshipApplicationService, LoginService } from "../../services";

export default function CompanyInternshipApplications() {
  const [pagedInternshipApplications, setPagedInternshipApplications] =
    useState(new PagedList({}));
  const [searchParams, setSearchParams] = useSearchParams();
  const [filterData, setFilterData] = useState({
    pageNumber: +(searchParams.get("pageNumber") ?? 1),
    firstName: searchParams.get("firstName") || "",
    lastName: searchParams.get("lastName") || "",
  });
  const internshipApplicationService = new InternshipApplicationService();
  const loginService = new LoginService();

  const refreshInternshipApplications = async () => {
    const data = await internshipApplicationService.getUnacceptedAsync({
      ...filterData,
      sortBy: "Id",
      sortOrder: "ASC",
      pageSize: 3,
      companyId: loginService.getUserToken().id,
    });
    console.log(data);
    setPagedInternshipApplications(data);
  };

  useEffect(() => {
    refreshInternshipApplications();
  }, [searchParams, filterData]);

  return (
    <div>
      <NavigationBar />

      <div className={"container"}>
        <div className="text-center">
          <h1>Internship applications</h1>
        </div>

        <InternshipApplicationFilter
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
        {pagedInternshipApplications.listSize === 0 && <NoItems />}
        {pagedInternshipApplications.data.map((internshipApplication) => (
          <InternshipApplicationComponent
            key={internshipApplication.id}
            onChange={() => {
              setFilterData({ ...filterData });
            }}
            internshipApplication={internshipApplication}
          />
          // <Internship
          //   key={internship.id}
          //   internship={internship}
          //   buttonText={"Details"}
          //   hasApplicationsCount={true}
          //   redirectTo={() => {
          //     navigate(
          //       `/internship/details/${internship.id}/${
          //         loginService.getUserToken().id
          //       }`
          //     );
          //   }}
          // />
        ))}
        <Paging
          currentPage={pagedInternshipApplications.currentPage}
          lastPage={pagedInternshipApplications.lastPage}
          onPageChanged={(page) => {
            setSearchParams({ ...filterData, pageNumber: page });
            setFilterData({ ...filterData, pageNumber: page });
          }}
        />
      </div>
    </div>
  );
}
