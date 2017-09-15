using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NackaPizzaOnline.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbOptions.UseSqlServer("Data Source=DESKTOP-2OVQA65;Initial Catalog=Pizzeria;User ID=pizzeria;Password=Pa$$w0rd;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            var db = new ApplicationDbContext(dbOptions.Options);
            return db;
        }
    }
}
