import React, { useState } from "react";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import {
  AdminNavigation,
  CompanyAdminList,
  CompanyFilterComponent,
  Paging,
} from "../../components";
import { PagedList } from "../../models";
import { CompanyService } from "../../services";

export default function AdminHomePage() {
  const companyService = new CompanyService();
  const [pagedCompanies, setPagedCompanies] = useState(new PagedList({}));
  const [currentFilter, setCurrentFilter] = useState({});
  const navigate = useNavigate();

  async function refreshCompanies({ pageNumber, ...filter }) {
    const data = await companyService.getAsync({
      pageNumber,
      ...filter,
      isAccepted: false,
    });
    setPagedCompanies(data);
  }

  useEffect(() => {
    refreshCompanies({ pageNumber: 1 });
  }, []);

  return (
    <div className="bg-dark">
      <AdminNavigation />
      <h1 className="text-light">Unaccepted companies</h1>
      <CompanyFilterComponent
        onFilter={(filter) => {
          setCurrentFilter(filter);
          refreshCompanies({ pageNumber: 1, ...filter });
        }}
        onClearFilter={() => refreshCompanies({ pageNumber: 1 })}
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
          refreshCompanies({ pageNumber: page });
        }}
      />
    </div>
  );
}
