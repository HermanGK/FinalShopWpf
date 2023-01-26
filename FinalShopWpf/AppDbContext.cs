using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalShopWpf.Model;
using Microsoft.EntityFrameworkCore;

namespace FinalShopWpf
{
    internal class AppDbContext:DbContext
    {
        public DbSet<Items> Items { get; set; }
        public DbSet<Users> Users { get; set; }

        public AppDbContext()
        {

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql("server=localhost;password=root;port=3306;username=root;database=ItemsForShop");
        }

    }
}
