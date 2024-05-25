using CitusTesting.DbContexts;
using CitusTesting.Models;

namespace CitusTesting.Seeders
{
    public class TransactionSeeder : IDatabaseSeeder
    {
        public uint TransactionsToGenerate { get; set; }

        public TransactionSeeder()
        {
            TransactionsToGenerate = 10;
        }

        public TransactionSeeder(uint transactionsToGenerate)
        {
            TransactionsToGenerate = transactionsToGenerate;
        }

        public override void Seed(AccountingContext context)
        {
            Console.WriteLine("Seeding transactions and entries");
            if (context.Transactions.Any())
            {
                Console.WriteLine("Transactions table already seeded");
                return;
            }

            if (!context.Facilities.Any())
            {
                Console.WriteLine("No facilities to generate transactions for");
                return;
            }

            Random random = new Random();

            List<Facility> facilities = context.Facilities.ToList();

            for (int i = 0; i < TransactionsToGenerate; i++)
            {
                // Choose a random facility
                Facility f = facilities[random.Next(facilities.Count)];

                //Console.WriteLine($"Creating transaction for {f.Name}");

                // Find two distinct random accounts for this facility
                List<Account> accounts = context.Accounts.Where(a => a.FacilityId == f.Id).ToList();
                int toCreditIndex = random.Next(accounts.Count);
                int toDebitIndex = random.Next(accounts.Count);
                while (toCreditIndex == toDebitIndex)
                {
                    toDebitIndex = random.Next(accounts.Count);
                }

                // Generate a random amount for this transaction
                int transactionAmount = random.Next(100000);

                // Create a new transaction for this facility
                Transaction t = new Transaction()
                {
                    FacilityId = f.Id
                };
                context.Transactions.Add(t);
                context.SaveChanges();

                // Create a credit entry to one account
                context.Entries.Add(new Entry()
                {
                    FacilityId = f.Id,
                    TransactionId = t.Id,
                    AccountId = accounts[toCreditIndex].Id,
                    Credit = transactionAmount,
                    Debit = 0
                });

                // Create a debit entry to the other
                context.Entries.Add(new Entry()
                {
                    FacilityId = f.Id,
                    TransactionId = t.Id,
                    AccountId = accounts[toDebitIndex].Id,
                    Credit = 0,
                    Debit = transactionAmount
                });

                 context.SaveChanges();
            }
        }
    }
}
