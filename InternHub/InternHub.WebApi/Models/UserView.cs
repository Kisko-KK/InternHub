using InternHub.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InternHub.WebApi.Models
{
    public class UserView
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Description { get; set; } = "";
        public CountyView County { get; set; }

        public UserView() { }

        public UserView(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Address = user.Address;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Description = user.Description;
            if(user.County != null) County = new CountyView(user.County);
        }
    }
}