using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using musicList2.Models;

namespace musicList2.Database
{
    public class SQLiteDbContext : DbContext
    {
        public DbSet<ListEntry<string>> Entries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "Database.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}
