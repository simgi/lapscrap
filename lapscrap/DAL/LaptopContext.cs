using lapscrap.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace lapscrap.DAL
{
    public class LaptopContext : DbContext
    {
        public LaptopContext() : base("LaptopContext")
        {
        }

        public DbSet<Laptop> Laptops{ get; set; }
        public DbSet<EbayItem> EbayItems{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}