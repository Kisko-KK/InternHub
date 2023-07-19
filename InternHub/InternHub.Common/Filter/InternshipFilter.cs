using System;
using System.Collections.Generic;

namespace InternHub.Common.Filter
{
    public class InternshipFilter
    {
        public InternshipFilter(List<Guid> counties, DateTime? startDate, DateTime? endDate, string name, bool? isActive)
        {
            Counties = counties;
            StartDate = startDate;
            EndDate = endDate;
            Name = name;
            IsActive = isActive;
        }
        public List<Guid> Counties { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
}
