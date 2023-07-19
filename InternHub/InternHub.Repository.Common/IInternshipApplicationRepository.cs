﻿using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Repository.Common
{
    public interface IInternshipApplicationRepository
    {
        Task<PagedList<InternshipApplication>> GetAllInternshipApplicationsAsync(Paging paging, Sorting sorting, InternshipApplicationFilter internshipApplicationFilter);
        Task<InternshipApplication> GetInternshipApplicationByIdAsync(Guid id);
        Task<bool> PostInternshipApplicationAsync(InternshipApplication internshipApplication);
       
    }
}