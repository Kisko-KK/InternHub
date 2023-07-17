using InternHub.Common;
using InternHub.Common.Filter;
using InternHub.Model;
using InternHub.Repository.Common;
using InternHub.Service.Common;
using System;
using System.Threading.Tasks;

namespace InternHub.Service
{
    public class InternshipService : IInternshipService
    {
        private IInternshipRepository InternshipRepository { get; set; }
        public InternshipService(IInternshipRepository internshipRepository)
        {
            InternshipRepository = internshipRepository;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await InternshipRepository.DeleteAsync(id);
        }

        public Task<PagedList<Internship>> GetAsync(Sorting sorting, Paging paging, CompanyFilter filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Internship> GetAsync(Guid id)
        {
            return await InternshipRepository.GetAsync(id);
        }

        public async Task<Internship> GetInternshipAsync(Guid id)
        {
            return await InternshipRepository.GetInternshipAsync(id);
        }

        public async Task<bool> PostAsync(Internship internship, string userId)
        {
            internship.Id = Guid.NewGuid();
            internship.DateCreated = DateTime.Now;
            internship.DateUpdated = DateTime.Now;
            internship.CreatedByUserId = userId;

            return await InternshipRepository.PostAsync(internship); 
        }

        public Task<bool> PutAsync(Internship internship, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
