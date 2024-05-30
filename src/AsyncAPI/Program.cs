using AsyncAPI.DbContexts;
using AsyncAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AsyncAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddDbContext<AccountingContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration.GetConnectionString("accounting_mssql"));
                options.UseNpgsql(builder.Configuration.GetConnectionString("accounting_multi"));
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
