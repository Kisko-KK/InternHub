namespace InternHub.WebApi.Migrations
{
    using InternHub.Model;
    using InternHub.WebApi.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InternHub.WebApi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InternHub.WebApi.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (!context.Users.Any(u => u.UserName == "admin@mono.com"))
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@mono.com",
                    Email = "admin@mono.com",
                    Address = "IT Park 1",
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Description = "I am the admin",
                    IsActive = true,
                    PasswordHash = new PasswordHasher().HashPassword("123456")
                };
                user.Id = "0c2ba6ff-9145-43cf-ac48-ccf7effea536";
                context.Users.Add(user);
            }

            //    var password = new PasswordHasher<ApplicationUser>();
            //    var hashed = password.HashPassword(user, "secret");
            //    user.PasswordHash = hashed;

            //    context.Users.Add(user);           
        }
    }
}
