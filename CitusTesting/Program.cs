using CitusTesting.Models;
using CitusTesting.Seeders;
using System.Diagnostics;

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
                new TransactionSeeder(1000)
            };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (IDatabaseSeeder seeder in seeders)
            {
                seeder.Seed(context);
            }
            stopwatch.Stop();

            Console.WriteLine($"Seeding finished -- {stopwatch.Elapsed}");

            //List<Account> accounts = context.Accounts.ToList();

            //foreach (Account account in accounts)
            //{
            //    Console.WriteLine($"{account.Id} - {account.Name} - {account.Number} - {(account.Type ? "Credit Normal" : "Debit Normal")} - {account.FacilityId}");
            //}
        }
    }
}
