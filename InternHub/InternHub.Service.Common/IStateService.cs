using InternHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Service.Common
{
    public interface IStateService
    {
        Task<List<State>> GetAllAsync();
        Task<State> GetByIdAsync(Guid id);
        Task<bool> Add(State state, string currentUserId);
        Task<bool> Update(State state, string currentUserId);
        Task<bool> Remove(Guid id);
    }
}
