using System.Reflection;
using System.Text.Json;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public static class MedicLabContextSeed
    {
        public static async Task SeedUsers(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!await roleManager.Roles.AnyAsync())
            {
                var roles = new List<Role>
            {
                new() { Name = "admin" },
                new() { Name = "employee" }
            };

                foreach (var role in roles)
                {
                    var result = await roleManager.CreateAsync(role);

                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to create role {role.Name}: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                    }
                }

                if (!await userManager.Users.AnyAsync())
                {
                    var usersData = File.ReadAllText(Path.Combine(path!, "Data", "SeedData", "users.json"));
                    var users = JsonSerializer.Deserialize<List<User>>(usersData);

                    if (users == null) return;

                    foreach (var user in users)
                    {
                        var result = await userManager.CreateAsync(user, "test");
                        if (!result.Succeeded)
                        {
                            throw new Exception($"Failed to create user {user.UserName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }

                        var role = user.UserName == "admin" ? "admin" : "employee";
                        result = await userManager.AddToRoleAsync(user, role);
                        if (!result.Succeeded)
                        {
                            throw new Exception($"Failed to add user {user.UserName} to role {role}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                        }
                    }
                }
            }
        }
    }
}