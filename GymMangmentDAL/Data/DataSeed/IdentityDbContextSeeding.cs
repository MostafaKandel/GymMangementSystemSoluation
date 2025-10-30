using GymMangmentDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymMangmentDAL.Data.DataSeed
{
    public static class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var HasUsers= userManager.Users.Any();
                var HasRoles = roleManager.Roles.Any();

                if (HasUsers && HasRoles) return false;
                if (!HasRoles)
                {
                    var Roles = new List<IdentityRole>()
                    {
                        new(){Name="Admin"},
                        new(){Name="SuperAdmin"}
                    };
                    
                    foreach (var role in Roles)
                    {
                        var roleExists = roleManager.RoleExistsAsync(role.Name!).Result;
                        if (!roleExists)
                        {
                             roleManager.CreateAsync(role).Wait();
                        }
                    }

                }

                if (!HasUsers)
                {
                    var MainAdmin = new ApplicationUser()
                    {
                        FirstName = "Mostafa ",
                        LastName = "Kandel",
                        UserName = "MostafaKandel",
                        Email = "mostafakandel@gmail.com",
                        PhoneNumber = "01150391463"
                    };
                    userManager.CreateAsync(MainAdmin, "@Ma24092492").Wait();
                    userManager.AddToRoleAsync(MainAdmin, "SuperAdmin").Wait();

                    var Admin = new ApplicationUser()
                    {
                        FirstName = "Mostafa",
                        LastName = "Anwar",
                        UserName = "MostafaAnwar",
                        Email = "mostafaanwar@gmail.com",
                        PhoneNumber = "01150391462"
                    };
                    userManager.CreateAsync(Admin, "@Ma24092492").Wait();
                    userManager.AddToRoleAsync(Admin, "Admin").Wait();
                }
                return true;
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed {ex}");
                return false;
            }
        }
    }
}
