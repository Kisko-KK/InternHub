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
    public class CountyService : ICountyService
    {
        public ICountyRepository Repo { get; set; }

        public CountyService(ICountyRepository repository)
        {
            Repo = repository;
        }

        public async Task<List<County>> GetAllAsync()
        {
            return await Repo.GetAllAsync();
        }

        public async Task<County> GetByIdAsync(Guid id)
        {
            return await Repo.GetByIdAsync(id);
        }

        public async Task<bool> Add(County county, string currentUserId)
        {
            county.DateCreated = county.DateUpdated = DateTime.UtcNow;
            county.CreatedByUserId = county.UpdatedByUserId = currentUserId;
            return await Repo.Add(county);
        }

        public async Task<bool> Update(County county, string currentUserId)
        {
            county.DateUpdated = DateTime.UtcNow;
            county.UpdatedByUserId = currentUserId;
            return await Repo.Update(county);
        }

        public async Task<bool> Remove(Guid id)
        {
            return await Repo.Remove(id);
        }
    }
}
