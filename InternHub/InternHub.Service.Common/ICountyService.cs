using InternHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Service.Common
{
    public interface ICountyService
    {
        Task<List<County>> GetAllAsync();
        Task<County> GetByIdAsync(Guid id);
        Task<bool> Add(County county, string currentUserId);
        Task<bool> Update(County county, string currentUserId);
        Task<bool> Remove(Guid id);
    }
}
