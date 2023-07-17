using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Model
{
    public class InternshipApplicationView
    {
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }


        public InternshipApplicationView(InternshipApplication internshipApplication)
        {
            DateCreated=internshipApplication.DateCreated;  
            DateUpdated=internshipApplication.DateUpdated;  
        }  
    }
}
