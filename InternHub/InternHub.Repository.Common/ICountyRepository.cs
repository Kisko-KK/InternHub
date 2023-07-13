using InternHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Repository.Common
{
    public interface ICountyRepository
    {
        Task<List<County>> GetAllAsync();
        Task<County> GetByIdAsync(Guid id);
        Task<bool> Add(County county);
        Task<bool> Update(County county);
        Task<bool> Remove(Guid id);
    }
}
