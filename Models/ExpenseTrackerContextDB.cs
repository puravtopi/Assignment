using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;

namespace ExpenseTracker.Models
{
    public class ExpenseTrackerContextDB : DbContext
    {

        public ExpenseTrackerContextDB(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<UserCategoryMap> UserCategoryMaps { get; set; }
        public DbSet<Expenses> Expenses { get; set; }

    }
}
