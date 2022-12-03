﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proiect_DAW.Data;
using System.Collections.Generic;

namespace Proiect_DAW.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService <DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Roles.Any())
                {
                    return; 
                }
                context.Roles.AddRange(
                new IdentityRole { Id = "2c5e174e-3b0e-446f-86af483d56fd8210", Name = "Admin", NormalizedName = "Admin".ToUpper() },
                new IdentityRole { Id = "2c5e174e-3b0e-446f-86af483d56fd8212", Name = "User", NormalizedName = "User".ToUpper() }
                );

                var hasher = new PasswordHasher<ApplicationUser>();
                context.Users.AddRange(
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdf0",
                    UserName = "admin@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "ADMIN@TEST.COM",
                    Email = "admin@test.com",
                    NormalizedUserName = "ADMIN@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "Admin1!")
                },
               
                new ApplicationUser
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdf2",
                    UserName = "user@test.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "USER@TEST.COM",
                    Email = "user@test.com",
                    NormalizedUserName = "USER@TEST.COM",
                    PasswordHash = hasher.HashPassword(null, "User1!")
                }
                );
                context.UserRoles.AddRange(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af483d56fd8210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdf0"
                },
                
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af483d56fd8212",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdf2"
                }
                );
                context.SaveChanges();
            }
        }
    }
}

