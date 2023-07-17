using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternHub.WebApi.Models
{
    public class CompanyUpdate
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public Guid CountyId { get; set; }
        public string Email { get; set; }
    }
}