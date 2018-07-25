namespace Events.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class DbMigrationsConfig : DbMigrationsConfiguration<Events.Data.ApplicationDbContext>
    {
        public DbMigrationsConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        public void CreateAdminUser(ApplicationDbContext context, String adminFullName, String adminEmail, String adminUserName, String adminPassword, String adminRole)
        {
            ApplicationUser admin = new ApplicationUser()
            {
                FullName = adminFullName,
                Email = adminEmail,
                UserName = adminUserName

            };
            
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            userManager.PasswordValidator = new PasswordValidator()
            {
                RequireDigit = false,
                RequiredLength = 1,
                RequireLowercase = false,
                RequireNonLetterOrDigit=false,
                RequireUppercase=false

            };

            var resp = userManager.Create(admin, adminPassword);
            if (!resp.Succeeded)
                throw new Exception(String.Join(";", resp.Errors));

            var roleManager = new RoleManager<IdentityRole>( new RoleStore<IdentityRole>(context));

            var respRole = roleManager.Create(new IdentityRole(adminRole));
            if (!respRole.Succeeded)
                throw new Exception(String.Join(";", respRole.Errors));

            var respAdd= userManager.AddToRole(admin.Id, adminRole);
            if (!respAdd.Succeeded)
                throw new Exception(String.Join(";", respAdd.Errors));



        }

        public void CreateSeveralEvents(ApplicationDbContext dbContext )
        {
            dbContext.Events.Add(new Event() {
                Title = "First Event",
                StartDateTime = DateTime.Now.AddDays(1).AddHours(-5),
                Author=dbContext.Users.First(),
                Description="This is my big first event"
            });

            dbContext.Events.Add(new Event()
            {
                Title = "Second Event",
                StartDateTime = DateTime.Now.AddDays(2).AddHours(-5),
                Author = dbContext.Users.First(),
                Description = "This is my big first event"
            });

            dbContext.Events.Add(new Event()
            {
                Title = "Big Event",
                StartDateTime = DateTime.Now.AddDays(3).AddHours(-5),
                Author = dbContext.Users.First(),
                Description = "This is my big first event"
            });

            dbContext.Events.Add(new Event()
            {
                Title = "Another event",
                StartDateTime = DateTime.Now.AddDays(4).AddHours(-5),
                Author = dbContext.Users.First(),
                Description = "This is my big first event"
            });

            dbContext.Events.Add(new Event()
            {
                Title = "Passed Event <annonymous>",
                StartDateTime = DateTime.Now.AddDays(-1).AddHours(-5),
                Comments = new HashSet<Comment>()
                    {
                        new Comment() { Text = "<Anonymous> comment", },
                        new Comment() { Text = "User comment", Author = dbContext.Users.First() }
                    },
            
            });

            dbContext.Events.Add(new Event()
            {
                Title = "Passed last event",
                StartDateTime = DateTime.Now.AddDays(-2).AddHours(-5),
                Author = dbContext.Users.First(),
                Description = "This is my big first event"
            });


            dbContext.Events.Add(new Event()
            {
                Title = "Past second event",
                StartDateTime = DateTime.Now.AddDays(-3).AddHours(-5),
                Author = dbContext.Users.First(),
                Description = "This is my big first event"
            });


            dbContext.Events.Add(new Event()
            {
                Title = "Past Third event",
                StartDateTime = DateTime.Now.AddDays(-4).AddHours(-5),
                Author = dbContext.Users.First(),
                Description = "This is my big first event"
            });


            dbContext.Events.Add(new Event()
            {
                Title = "Another past event",
                StartDateTime = DateTime.Now.AddDays(-5).AddHours(-5),
                Author = dbContext.Users.First(),
                Description = "This is my big first event"
            });


        }

        protected override void Seed(Events.Data.ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                CreateAdminUser(context,"System Admin","admin@admin.com","admin@admin.com", "admin@admin.com", "Administrator");
                
            }

            if (!context.Events.Any())
            {
                CreateSeveralEvents(context);
            }
        }
    }
}
