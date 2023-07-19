using InternHub.WebApi.Models;
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
        public string Message { get; set; }

        public StudentView StudentView { get; set; }    
        public StateView StateView { get; set; }

        public InternshipApplicationView(InternshipApplication internshipApplication)
        {
            Message=internshipApplication.Message;
            DateCreated=internshipApplication.DateCreated;  
            DateUpdated=internshipApplication.DateUpdated;
            StudentView = new StudentView(internshipApplication.Student);
            StateView = new StateView(internshipApplication.State);
        }  
    }
}
