using InternHub.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Model
{
    public class Student : User, IStudent
    {
        public Guid StudyAreaId { get; set; }
        public StudyArea StudyArea { get; set; }
    }
}
