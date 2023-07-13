using InternHub.Model;
using InternHub.Model.Common;
using InternHub.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Repository
{
    public class StateRepository : IStateRepository
    {
        private IConnectionString ConnectionString { get; }

        public StateRepository(IConnectionString connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool Add()
        {
            return true;
        }
    }
}
