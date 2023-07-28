import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  AdminNavigation,
  Button,
  CompanyAdminFilter,
  CompanyList,
  Paging,
} from "../../components";
import { PagedList } from "../../models";
import { CompanyService } from "../../services";

export default function AdminCompaniesPage() {
  const companyService = new CompanyService();
  const [pagedCompanies, setPagedCompanies] = useState(new PagedList({}));
  const [currentFilter, setCurrentFilter] = useState({});
  const navigate = useNavigate();

  async function refreshCompanies({ pageNumber, ...filter }) {
    const data = await companyService.getAsync({
      pageNumber,
      ...filter,
    });
    setPagedCompanies(data);
  }

  useEffect(() => {
    refreshCompanies({ pageNumber: 1 });
  }, []);

  return (
    <div className="bg-dark">
      <AdminNavigation />
      <h1 className="text-light">Companies</h1>
      <CompanyAdminFilter
        onFilter={(filter) => {
          refreshCompanies({ pageNumber: 1, ...filter });
        }}
        onClearFilter={() => {
          setCurrentFilter({});
          refreshCompanies({ pageNumber: 1 });
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
          refreshCompanies({
            pageNumber: pagedCompanies.currentPage,
            ...currentFilter,
          })
        }
      />
      <Paging
        currentPage={pagedCompanies.currentPage}
        lastPage={pagedCompanies.lastPage}
        onPageChanged={(page) => {
          refreshCompanies(page);
        }}
      />
    </div>
  );
}
