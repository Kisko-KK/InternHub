﻿using InternHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Repository.Common
{
    public interface IStateRepository
    {
        Task<List<State>> GetAllAsync();
        Task<State> GetByIdAsync(Guid id);
        Task<bool> Add(State state);
        Task<bool> Update(State state);
        Task<bool> Remove(Guid id);
    }
}