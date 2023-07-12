﻿using System;
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
    }
}
