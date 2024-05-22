using CitusTesting.Models;
using CitusTesting.Seeders;

namespace CitusTesting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            AccountingContext context = new AccountingContext(config);

            IDatabaseSeeder[] seeders = new IDatabaseSeeder[]
            {
                new FacilitiesSeeder(),
                new AccountsSeeder(),
            };

            foreach (IDatabaseSeeder seeder in seeders)
            {
                seeder.Seed(context);
            }

            List<Account> accounts = context.Accounts.ToList();

            foreach (Account account in accounts)
            {
                Console.WriteLine($"{account.Id} - {account.Name} - {account.Number} - {(account.Type ? "Credit Normal" : "Debit Normal")} - {account.FacilityId}");
            }
            Console.ReadKey();
        }
    }
}
