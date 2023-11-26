using Microsoft.AspNetCore.Identity;
using ShawahinAPI.Core.Entities;
using ShawahinAPI.Core.Enums;

namespace ShawahinAPI.Application
{

    public static class RoleInitializer
    {
        public static async Task Initialize(RoleManager<ApplicationRole> roleManager)
        {
            await CreateRole(roleManager, UserRole.Admin);
            await CreateRole(roleManager, UserRole.User);
            await CreateRole(roleManager, UserRole.Moderator);
        }

        private static async Task CreateRole(RoleManager<ApplicationRole> roleManager, UserRole roleName)
        {
            string role = roleName.ToString();

            if (!await roleManager.RoleExistsAsync(role))
            {
                var newRole = new ApplicationRole(role.ToString());
                var result = await roleManager.CreateAsync(newRole);

                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Error creating role {role}");
                }
            }
        }
    }


}
