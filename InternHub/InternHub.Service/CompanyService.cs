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
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository CompanyRepository { get; set; }
        public CompanyService(ICompanyRepository companyRepository) {
            CompanyRepository = companyRepository;
        }
        public async Task<int> DeleteAsync(string id)
        {
            return await CompanyRepository.DeleteAsync(id);
        }

        public async Task<PagedList<Company>> GetAsync(Sorting sorting, Paging paging)
        {
            return await CompanyRepository.GetAsync(sorting, paging);
        }

        public async Task<Company> GetAsync(string id)
        {
            return await CompanyRepository.GetAsync(id);
        }

        public async Task<Company> GetCompanyAsync(string id)
        {
            return await CompanyRepository.GetCompanyAsync(id);
        }

        public async Task<int> PostAsync(Company company)
        {
            company.DateCreated = DateTime.Now;
            company.Id = Guid.NewGuid().ToString();
            return await CompanyRepository.PostAsync(company);
        }

        public async Task<int> PutAsync(Company company)
        {
            company.DateUpdated = DateTime.Now;
            return await CompanyRepository.PutAsync(company);
        }

        public Task<int> AcceptAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
