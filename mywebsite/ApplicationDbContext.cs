using JewelryStore.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JewelryStore.UI
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("StoreContext", throwIfV1Schema: false)
        {
        }

        public DbSet<Jewelry> Jewelries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Registration> Registrations { get; set; }
               
       

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}