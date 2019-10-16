using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using musicList2.Models;

namespace musicList2.Database
{
    public class SQLiteDbContext : DbContext
    {
        public DbSet<ListEntry<string>> Entries { get; set; }

        private IConfiguration configuration;

        public SQLiteDbContext(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration.GetConnectionString("SQLite");
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}
