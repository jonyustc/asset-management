using Core.Model;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class SeedData
    {
        public static async Task SeedAsync(DataContext context)
        {
            if(!context.Companies.Any())
            {
                var company = new Company{
                    CompanyName = "Test Asset Company",
                    Address = "Test Address",
                    Country = "Bangladesh",
                    City = "Chittagong",
                    State = "Chittagong",
                    Street = "12/A House Steet",
                    PostalCode = "4000",
                    Email = "abc@test.com",
                    Mobile = "+8801815887596",
                    LogoUrl = "images/logo.png"
                    
                };

                context.Companies.Add(company);

                await context.SaveChangesAsync();
            }

            // if(!context.Sites.Any())
            // {
            //     var site = new Site{
            //         Name = "Company Site 1",
            //         Address = "Test Address",
            //         City = "Chittagong",
            //         State = "Chittagong",
            //         Street = "12/A House Steet",
            //         PostalCode = "4000",
            //         Mobile = "+8801815887596",
            //         CopmanyId = 1,

            //     };

            //     //context.Sites.Add(site);

            //     await context.SaveChangesAsync();
            // }

            //  if(!context.Locations.Any())
            // {
            //     var location = new Location{
            //         Name = "Site Location 1",
            //         Description = "Site Description",
            //         SiteId = 1

            //     };

            //     context.Locations.Add(location);

            //     await context.SaveChangesAsync();
            // }

            // if(!context.Categories.Any())
            // {
            //     var catetory = new List<Category>{
            //         new Category{
            //             Name = "Computer equipment",
            //             Description = "Test Description",
            //         },
            //          new Category{
            //             Name = "Furniture and fixtures",
            //             Description = "Test Description",
            //         },
            //          new Category{
            //             Name = "Software",
            //             Description = "Test Description",
            //         },
            //          new Category{
            //             Name = "Vehicles",
            //             Description = "Test Description",
            //         },
            //          new Category{
            //             Name = "Intangible assets",
            //             Description = "Test Description",
            //         }

            //     };

            //     context.Categories.AddRange(catetory);

            //     await context.SaveChangesAsync();
            // }

            //  if(!context.departments.Any())
            // {
            //     var dept = new List<Department>{
            //         new Department{
            //             Name = "Accounting",
            //             Description = "Test Description",
            //         },
            //          new Department{
            //             Name = "Marketing",
            //             Description = "Test Description",
            //         },
            //          new Department{
            //             Name = "Executive",
            //             Description = "Test Description",
            //         },
            //          new Department{
            //             Name = "Programmer",
            //             Description = "Test Description",
            //         },
            //          new Department{
            //             Name = "Designer",
            //             Description = "Test Description",
            //         },
            //         new Department{
            //             Name = "IT Officer",
            //             Description = "Test Description",
            //         }

            //     };

            //     context.departments.AddRange(dept);

            //     await context.SaveChangesAsync();
            // }
        }

        public static async Task IdentitySeedAsync(UserManager<ApplicationUser> userManager,RoleManager<Role> roleManager)
        {

            if(!roleManager.Roles.Any())
            {
                var roles = new List<Role>()
                { 
                    new Role { Name = "Admin" },
                    new Role { Name = "Manager" },
                    new Role { Name = "Technical Analysts" }
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            if(!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    FirstName = "Robert",
                    LastName = "Smith",
                    DisplayName = "Admin",
                    Email = "admin@test.com",
                    UserName = "admin@test.com",
                    CopmanyId =1
                };

                var result = await userManager.CreateAsync(user,"Pa$$w0rd");

                if(result.Succeeded)
                {

                    var userData = await userManager.FindByEmailAsync(user.Email);
                   await userManager.AddToRoleAsync(userData,"Admin");
                }

                var userModarotor = new ApplicationUser
                {
                    FirstName = "Adam",
                    LastName = "Smith",
                    DisplayName = "Manager",
                    Email = "manager@test.com",
                    UserName = "manager@test.com",
                    CopmanyId =1
                };

                var resultModarator = await userManager.CreateAsync(userModarotor,"Pa$$w0rd");

                if(resultModarator.Succeeded)
                {
                     var userData = await userManager.FindByEmailAsync(userModarotor.Email);
                   await userManager.AddToRoleAsync(userData,"Manager");
                }

                var userMember = new ApplicationUser
                {
                    FirstName = "Jhon",
                    LastName = "Smith",
                    DisplayName = "Technical Analysts",
                    Email = "technical.analysts@test.com",
                    UserName = "technical.analysts@test.com",
                    CopmanyId =1
                };

                var resultMember = await userManager.CreateAsync(userMember,"Pa$$w0rd");

                if(resultMember.Succeeded)
                {
                    var userData = await userManager.FindByEmailAsync(userMember.Email);
                   await userManager.AddToRoleAsync(userData,"Technical Analysts");
                }

            }


            

        }
    }
}