using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Models;

namespace FoodDelivery.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FoodDelivery.Models.Category> Category { get; set; }
        public DbSet<FoodDelivery.Models.FoodType> FoodType { get; set; }
        public DbSet<FoodDelivery.Models.MenuItem> MenuItem { get; set; }
    }
}
