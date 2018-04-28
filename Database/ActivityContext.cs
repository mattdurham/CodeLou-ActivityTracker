using ActivityTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ActivityTracker.Database
{
    /// <summary>
    /// Database context to connect to the database.
    /// </summary>
    public class ActivityContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQLite for Dev, will use something else for production. SQLite is GREAT for local development if you can get away with it
            optionsBuilder.UseSqlite("Data Source=activity.db");
        }
    }
}
