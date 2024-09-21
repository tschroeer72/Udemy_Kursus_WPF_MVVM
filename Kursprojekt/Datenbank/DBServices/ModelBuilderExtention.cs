using Kursprojekt.Datenbank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.Datenbank.DBServices;

public static class ModelBuilderExtention
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(new Role 
        {
            ID = 1, 
            RoleName = RoleType.Admin.ToString()
        });

        string strPW = "123";
        using var sha256 = SHA256.Create();
        var PWByte = Encoding.Default.GetBytes(strPW);
        var PWByteSHA = sha256.ComputeHash(PWByte);
        var strPassword = Convert.ToBase64String(PWByteSHA);

        modelBuilder.Entity<AppUser>().HasData(new AppUser 
        {
            ID = 1, 
            FirstName = "Thorsten",
            LastName = "Schröer",
            Email = "t.schroeer@web.de",
            Password = strPassword,
            RoleID = 1
        });

        foreach (var fkey in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
        {
            fkey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
