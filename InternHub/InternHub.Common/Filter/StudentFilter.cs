using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Common.Filter
{
    public class StudentFilter
    {
        public List<Guid> StudyAreas { get; set; }
        public List<Guid> Counties { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }

        public StudentFilter(string firstName, string lastName, List<Guid> studyAreas, List<Guid> counties, bool isActive)
        {
            StudyAreas = studyAreas;
            Counties = counties;
            FirstName = firstName;
            LastName = lastName;
            IsActive = isActive;
        }
    }
}
