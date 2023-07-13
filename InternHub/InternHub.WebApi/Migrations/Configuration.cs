namespace InternHub.WebApi.Migrations
{
    using InternHub.Model;
    using InternHub.WebApi.Models;
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

            //if (!context.Users.Any(u => u.UserName == "admin"))
            //{
            //    ApplicationUser user = new Models.ApplicationUser
            //    {
            //        UserName = "a@a.com",
            //        Email = "a@a.com",
            //    };

            //    var password = new PasswordHasher<ApplicationUser>();
            //    var hashed = password.HashPassword(user, "secret");
            //    user.PasswordHash = hashed;

            //    context.Users.Add(user);
            //}            
        }
    }
}
