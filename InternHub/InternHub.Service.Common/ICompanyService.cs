using InternHub.Common;
using InternHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Service.Common
{
    public interface ICompanyService
    {
        Task<PagedList<Company>> GetAsync(Sorting sorting, Paging paging);
        Task<Company> GetAsync(string id);
        Task<int> PostAsync(Company company);
        Task<int> PutAsync(Company company);
        Task<int> DeleteAsync(string id);
        Task<Company> GetCompanyAsync(string id);
        Task<int> AcceptAsync(string id);
    }
}
