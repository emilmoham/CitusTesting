using CitusTesting.DbContexts;
using CitusTesting.Services;
using Microsoft.EntityFrameworkCore;

namespace CitusTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AccountingContext>(options =>
            {
#if CITUS
                options.UseNpgsql(builder.Configuration.GetConnectionString("accounting_multi"));
#elif MSSQL
                options.UseSqlServer(builder.Configuration.GetConnectionString("accounting_mssql"));
#elif POSTGRESQL
                options.UseNpgsql(builder.Configuration.GetConnectionString("accounting_single"));
#endif
            });

            builder.Services.AddScoped<IFacilitiesRepository, FacilitiesRepository>();
            builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
            builder.Services.AddScoped<ITransactionsRepository, TransactionRepository>();
            builder.Services.AddScoped<IEntriesRepository, EntriesRepository>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
