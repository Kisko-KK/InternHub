using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Repository.Common;
using InternHub.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Service
{
    public class InternshipApplicationService : IInternshipApplicationService
    {
        public IInternshipApplicationRepository _repo;

        public InternshipApplicationService(IInternshipApplicationRepository internshipApplicationRepository)
        {
            _repo = internshipApplicationRepository;
        }


        public async Task<PagedList<InternshipApplication>> GetAllInternshipApplicationsAsync(Paging paging, Sorting sorting, InternshipApplicationFilter internshipApplicationFilter) => await _repo.GetAllInternshipApplicationsAsync(paging, sorting, internshipApplicationFilter);


        public async Task<InternshipApplication> GetInternshipApplicationByIdAsync(Guid id) => await _repo.GetInternshipApplicationByIdAsync(id);

        public async Task<bool> PostInternshipApplicationAsync(InternshipApplication internshipApplication, string currentUserId)
        {
            if (currentUserId == null)
            {
                return false;

            }
            if (internshipApplication == null)
            {
                return false;

            }
            internshipApplication.DateCreated = DateTime.Now;
            internshipApplication.DateUpdated = DateTime.Now;
            internshipApplication.CreatedByUserId = currentUserId;
            internshipApplication.UpdatedByUserId = currentUserId;
            return await _repo.PostInternshipApplicationAsync(internshipApplication);
        }
    }
}
