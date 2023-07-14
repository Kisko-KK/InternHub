using InternHub.Common;
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
      

        public Task<InternshipApplication> GetInternshipApplicationByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<InternshipApplication> PostInternshipApplicationAsync(InternshipApplication internshipApplication)
        {
            throw new NotImplementedException();
        }

        public Task<InternshipApplication> PutInternshipApplicationAsync(Guid id, InternshipApplication internshipApplication)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteInternshipApplicationAsync(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
