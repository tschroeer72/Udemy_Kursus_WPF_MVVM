using Kursprojekt.Datenbank.DBServices;
using Kursprojekt.Datenbank.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursprojekt.Datenbank;

#nullable disable
public class ApplicationDBContext : DbContext
{
    private readonly string _Constring = @"Server=(LocalDb)\MSSQLLocalDB; Database = ROOMSDB; Integrated Security = True; TrustServerCertificate=True";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_Constring);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SeedData();
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Role> Roles { get; set; }
}
