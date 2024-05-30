using AsyncAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;

namespace AsyncAPI.DbContexts
{
    public class AccountingContext : DbContext
    {
        public AccountingContext(DbContextOptions<AccountingContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
#if CITUS || POSTGRESQL
            var mapper = new NpgsqlSnakeCaseNameTranslator();
            var types = modelBuilder.Model.GetEntityTypes().ToList();

            // Refer to tables in snake_case internally
            types.ForEach(e => e.SetTableName(mapper.TranslateMemberName(e.GetTableName() ?? "error")));

            // Refer to columns in snake_case internally
            types.SelectMany(e => e.GetProperties())
               .ToList()
               .ForEach(p => p.SetColumnName(mapper.TranslateMemberName(p.GetColumnBaseName())));
#endif
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
