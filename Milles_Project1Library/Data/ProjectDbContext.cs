using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Milles_Project1Library.Models;

namespace Milles_Project1Library.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
        {

        }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }

        public DbSet<Calculator> Calculator { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Shape> Shape { get; set; }
        public DbSet<UserHistory> UserHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=localhost;initial catalog=Project1;integrated security=true;TrustServerCertificate=True;");
            }
        }
    }
}
