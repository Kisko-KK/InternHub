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

        public bool Add() => Repo.Add();
    }
}
