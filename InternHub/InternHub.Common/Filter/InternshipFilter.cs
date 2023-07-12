using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Common.Filter
{
    public class InternshipFilter
    {
        public List<Guid> Counties { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
    }
}
