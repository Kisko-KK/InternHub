using InternHub.Model;
using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternHub.WebApi.Models
{
    public class CompanyView
    {
        public CompanyView(Company company) {
            Name = company.Name;
            Website = company.Website;
            Address = company.Address;
            Id = company.Id;
        }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string Id { get; set; }
    }
}