using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Vulcanova.Features.Auth.Accounts;
using Vulcanova.Features.Grades;
using Vulcanova.Features.LuckyNumber;

namespace Vulcanova.Core.Data
{
    public class AppDbContext : DbContext
    {
        private readonly string _dbName;
        public DbSet<Account> Accounts { get; private set; }
        public DbSet<LuckyNumber> LuckyNumbers { get; private set; }
        public DbSet<Grade> Grades { get; private set; }

        public AppDbContext()
        {
        }

        public AppDbContext(string dbName)
        {
            _dbName = dbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // HACK: Check if migrations are being created
            optionsBuilder.UseSqlite(Assembly.GetCallingAssembly().FullName == "Vulcanova.Dummy"
                ? "Filename=fakeDb.db3"
                : $"Filename={_dbName}");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}