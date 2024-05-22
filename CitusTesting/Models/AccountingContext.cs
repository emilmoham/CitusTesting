using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;

namespace CitusTesting.Models
{
    public class AccountingContext : DbContext
    {
        private IConfiguration Configuration;
        
        public AccountingContext (IConfiguration configuration){
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("accounting_multi"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();
            var types = modelBuilder.Model.GetEntityTypes().ToList();

            // Refer to tables in snake_case internally
            types.ForEach(e => e.SetTableName(mapper.TranslateMemberName(e.GetTableName() ?? "error")));

            // Refer to columns in snake_case internally
            types.SelectMany(e => e.GetProperties())
                .ToList()
                .ForEach(p => p.SetColumnName(mapper.TranslateMemberName(p.GetColumnBaseName())));
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
