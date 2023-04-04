using E_Commerce_Database.Seller;
using E_Commerce_Database.Seller_Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Database
{
    public class dbContext : DbContext
    {

        public DbSet<Category> category { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<SellerDetail> sellerDetail { get; set; }
        public DbSet<SellerAddress> sellerAddress { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=WEIHOPE;Database=E-Commerce_Database;Trusted_Connection=True;");
        }

    }
}
