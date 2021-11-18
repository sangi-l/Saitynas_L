using Microsoft.AspNetCore.Identity;
using Saitynas_L1.Auth.Model;
using Saitynas_L1.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saitynas_L1.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task SeedAsync()
        {
            foreach (var role in UserRoles.All)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            var newAdminUser = new User
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };
            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "Administratorius1!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, UserRoles.All);
                }
            }
            var newWorkerUser = new User
            {
                UserName = "worker",
                Email = "worker@worker.com"
            };
            var existingWorkerUser = await _userManager.FindByNameAsync(newWorkerUser.UserName);
            if (existingWorkerUser == null)
            {
                var createWorkerUserResult = await _userManager.CreateAsync(newWorkerUser, "Darbuotojas1!");
                if (createWorkerUserResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newWorkerUser, UserRoles.Worker);
                }
            }
            var newUser = new User
            {
                UserName = "user",
                Email = "user@user.com"
            };
            var existingUser = await _userManager.FindByNameAsync(newUser.UserName);
            if (existingUser == null)
            {
                var createUserResult = await _userManager.CreateAsync(newUser, "Naudotojas1!");
                if (createUserResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }
    }
}
