using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Contexts
{
    public class APPDbContext : DbContext
    {
        //we dont want to expose the connection string in the context class
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connection string with diff way ");
        //}


        public APPDbContext(DbContextOptions<APPDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(APPDbContext).Assembly);//3shan ygeb al ssembly fe el project da bs 
        }

        public DbSet<Department> Departments { get; set; }
    }
}
