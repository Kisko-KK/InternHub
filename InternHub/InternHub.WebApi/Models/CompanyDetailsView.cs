using InternHub.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternHub.WebApi.Models
{
    public class CompanyDetailsView
    {
        public CompanyDetailsView(Company company)
        {
            Name = company.Name;
            Website = company.Website;
            Address = company.Address;
            Id = company.Id;
            Email = company.Email;
            CountyView = new CountyView(company.County);
            PhoneNumber = company.PhoneNumber; 
            Description=company.Description;    

        }
        public CompanyDetailsView() { }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }
        public string Id { get; set; }
        public CountyView CountyView { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
    }
}