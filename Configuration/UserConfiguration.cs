using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var hasher = new PasswordHasher<User>();

        var adminUser = new User
        {
            Id = 1,
            UserName = "admin@example.com",
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@example.com",
            Role = "Admin",
            NormalizedUserName = "ADMIN@EXAMPLE.COM", // Manually normalized
            NormalizedEmail = "ADMIN@EXAMPLE.COM"   // Manually normalized
        };
        adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin123!");

        var managerUser = new User
        {
            Id = 2,
            UserName = "manager@example.com",
            FirstName = "Manager",
            LastName = "User",
            Email = "manager@example.com",
            Role = "Manager",
            NormalizedUserName = "MANAGER@EXAMPLE.COM", // Manually normalized
            NormalizedEmail = "MANAGER@EXAMPLE.COM"   // Manually normalized
        };

        managerUser.PasswordHash = hasher.HashPassword(managerUser, "Manager123!");

        var employeeUser = new User
        {
            Id = 3,
            UserName = "employee@example.com",
            FirstName = "Employee",
            LastName = "User",
            Email = "employee@example.com",
            Role = "Employee",
            NormalizedUserName = "EMPLOYEE@EXAMPLE.COM", // Manually normalized
            NormalizedEmail = "EMPLOYEE@EXAMPLE.COM"   // Manually normalized
        };

        employeeUser.PasswordHash = hasher.HashPassword(employeeUser, "Employee123!");

        builder.HasData(adminUser, managerUser, employeeUser);
    }
}
