using InternHub.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace InternHub.WebApi.Models
{
    public class InternshipApplicationStudentsView
    {
        public InternshipApplicationStudentsView() { }

        public InternshipApplicationStudentsView(InternshipApplication internshipApplication)
        {
            Internship = new InternshipShortView(internshipApplication.Internship);
            Student = new StudentView(internshipApplication.Student);
            State = new StateView(internshipApplication.State);
            Message = internshipApplication.Message;
        }

        public string Message { get; set; }
        public InternshipShortView Internship { get; set; }
        public StudentView Student { get; set; }
        public StateView State { get; set; }
    }
}