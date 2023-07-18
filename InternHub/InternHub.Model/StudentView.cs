using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternHub.Model
{
    public class StudentView : User
    {
        public StudyArea StudyArea { get; set; }
        public Guid StudyAreaId { get; set; }

        public StudentView(Student student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            LastName = student.LastName;
            Email = student.Email;
            PhoneNumber = student.PhoneNumber;
            Address = student.Address;
            Description = student.Description;
            DateCreated = student.DateCreated;
            DateUpdated = student.DateUpdated;
            CountyId = student.CountyId;
            IsActive = student.IsActive;
            StudyArea = student.StudyArea;
        }
    }
}
