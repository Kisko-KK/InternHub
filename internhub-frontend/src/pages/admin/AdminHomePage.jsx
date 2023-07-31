import React, { useState } from "react";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import {
  NavigationBar,
  CompanyAdminList,
  CompanyFilterComponent,
  Paging,
} from "../../components";
import { PagedList } from "../../models";
import { CompanyService } from "../../services";
import { useSearchParams } from "react-router-dom/dist";

export default function AdminHomePage() {
  const companyService = new CompanyService();
  const [searchParams, setSearchParams] = useSearchParams();
  const [pagedCompanies, setPagedCompanies] = useState(new PagedList({}));
  const [currentFilter, setCurrentFilter] = useState({
    pageNumber: +(searchParams.get("pageNumber") ?? 1),
    name: searchParams.get("name"),
  });
  const navigate = useNavigate();

  async function refreshCompanies() {
    const data = await companyService.getAsync({
      ...currentFilter,
      isAccepted: false,
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
        <h1>Unaccepted companies</h1>
      </div>
      <CompanyFilterComponent
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
      <CompanyAdminList
        companies={pagedCompanies.data}
        onChange={() => {
          refreshCompanies({
            pageNumber: pagedCompanies.currentPage,
            ...currentFilter,
          });
        }}
      />
      <Paging
        currentPage={pagedCompanies.currentPage}
        lastPage={pagedCompanies.lastPage}
        onPageChanged={(page) => {
          setSearchParams({ ...currentFilter, pageNumber: page });
          setCurrentFilter({ ...currentFilter, pageNumber: page });
        }}
      />
    </div>
  );
}
