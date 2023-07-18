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
        public Student() { }
        public Guid StudyAreaId { get; set; }
        public StudyArea StudyArea { get; set; }
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public Guid CountyId { get; set; }

        public Student(string id, string firstName, string lastName, string email, string phoneNumber, string address, string description, DateTime dateCreated, DateTime dateUpdated, Guid countyId, bool isActive, Guid studyAreaId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Description = description;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
            CountyId = countyId;
            StudyAreaId = studyAreaId;
            IsActive = isActive;
        }
    }
}
