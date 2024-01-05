using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milles_Project1Library.Data
{
    public static class DbConfiguration
    {
        public static ProjectDbContext StartDatabase()
        {
            var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();

            var options = new DbContextOptionsBuilder<ProjectDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);

            var dbContext = new ProjectDbContext(options.Options);

            return dbContext;
        }
    }
}
