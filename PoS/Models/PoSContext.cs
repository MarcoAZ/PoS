using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PoS.Models
{
    public class PoSContext : DbContext 
    {
        public PoSContext() : base("PoSContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }

    }
}