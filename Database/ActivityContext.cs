using ActivityTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ActivityTracker.Database
{
    public class ActivityContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQLite for Dev, will use something else for production
            optionsBuilder.UseSqlite("Data Source=activity.db");
        }
    }
}
