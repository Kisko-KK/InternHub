using InternHub.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.WebApi.Models
{
    public class StudentView : UserView
    {
        public StudyAreaView StudyArea { get; set; }

        public StudentView() { }

        public StudentView(Student student) : base(student)
        {
            StudyArea = new StudyAreaView(student.StudyArea);
        }
    }
}
