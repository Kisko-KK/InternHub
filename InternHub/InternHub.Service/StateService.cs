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
    public class StateService : IStateService
    {
        public IStateRepository Repo { get; }

        public StateService(IStateRepository repository)
        {
            Repo = repository;
        }

        public async Task<List<State>> GetAllAsync()
        {
            return await Repo.GetAllAsync();
        }

        public async Task<State> GetByIdAsync(Guid id)
        {
            return await Repo.GetByIdAsync(id);
        }

        public async Task<bool> Add(State state, string currentUserId)
        {
            state.DateCreated = state.DateUpdated = DateTime.UtcNow;
            state.CreatedByUserId = state.UpdatedByUserId = currentUserId;
            return await Repo.Add(state);
        }

        public async Task<bool> Update(State state, string currentUserId)
        {
            state.DateUpdated = DateTime.UtcNow;
            state.UpdatedByUserId = currentUserId;
            return await Repo.Update(state);
        }

        public async Task<bool> Remove(Guid id)
        {
            return await Repo.Remove(id);
        }
    }
}
