import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  NavigationBar,
  Button,
  CompanyAdminFilter,
  CompanyList,
  Paging,
} from "../../components";
import { PagedList } from "../../models";
import { CompanyService } from "../../services";
import { useSearchParams } from "react-router-dom/dist";

export default function AdminCompaniesPage() {
  const companyService = new CompanyService();
  const [searchParams, setSearchParams] = useSearchParams();
  const [pagedCompanies, setPagedCompanies] = useState(new PagedList({}));
  const [currentFilter, setCurrentFilter] = useState({
    pageNumber: +(searchParams.get("pageNumber") ?? 1),
    name: searchParams.get("name"),
    isActive:
      searchParams.get("isActive") === null
        ? true
        : searchParams.get("isActive").toLowerCase() === "true",
    isAccepted:
      searchParams.get("isAccepted") === null
        ? false
        : searchParams.get("isAccepted").toLowerCase() === "true",
  });
  const navigate = useNavigate();

  async function refreshCompanies() {
    const data = await companyService.getAsync({
      ...currentFilter,
      sortBy: "Id",
      sortOrder: "ASC",
      pageSize: 10,
    });
    setPagedCompanies(data);
  }

  useEffect(() => {
    refreshCompanies();
  }, [searchParams, currentFilter]);

  return (
    <div>
      <NavigationBar />
      <div className="text-center">
        <h1>Companies</h1>
      </div>
      <CompanyAdminFilter
        filter={currentFilter}
        onFilter={(filter) => {
          setSearchParams({ ...filter, pageNumber: 1 });
          setCurrentFilter({ ...filter, pageNumber: 1 });
        }}
        onClearFilter={() => {
          setSearchParams({ pageNumber: 1 });
          setCurrentFilter({ pageNumber: 1 });
        }}
      />
      <Button
        buttonColor="success"
        onClick={() => {
          navigate("/company/register");
        }}
      >
        New company
      </Button>
      <CompanyList
        companies={pagedCompanies.data}
        onRemove={() =>
          setSearchParams({
            ...currentFilter,
            pageNumber: pagedCompanies.currentPage,
          })
        }
      />
      <Paging
        currentPage={pagedCompanies.currentPage}
        lastPage={pagedCompanies.lastPage}
        onPageChanged={(page) => {
          setSearchParams({ ...currentFilter, pageNumber: page });
          refreshCompanies();
        }}
      />
    </div>
  );
}
