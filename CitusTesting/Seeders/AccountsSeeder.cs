using CitusTesting.Models;
using System.Diagnostics;

namespace CitusTesting.Seeders
{
    


    public class AccountsSeeder : IDatabaseSeeder
    {
        internal class AccountSeed
        {
            public string Name { get; set; }
            public int Number { get; set; }
            public bool CreditNormal { get; set; }

            public AccountSeed(string name, int number, bool creditNormal)
            {
                Name = name;
                Number = number;
                CreditNormal = creditNormal;
            }
        }

        AccountSeed[] AccountDefinitions = new AccountSeed[]
        {
            new AccountSeed("Assets",       1000, false),
            new AccountSeed("Liabilities",  2000, true),
            new AccountSeed("Equity",       3000, true),
            new AccountSeed("Revenue",      4000, true),
            new AccountSeed("Expenses",     5000, false)
        };

        public override void Seed(AccountingContext context)
        {
            if (context.Accounts.Count() == context.Facilities.Count() * AccountDefinitions.Length)
            {
                Console.WriteLine("Accounts table already seeded");
                return;
            }

            List<Facility> facilities = context.Facilities.ToList();
            foreach (Facility facility in facilities)
            {
                Console.WriteLine($"{facility.Name} Accounts");
                foreach (AccountSeed definition in AccountDefinitions)
                {
                    Console.WriteLine($"\t{definition.Name}");
                    context.Accounts.Add(new Account
                    {
                        FacilityId = facility.Id,
                        Name = definition.Name,
                        Number = definition.Number,
                        Type = definition.CreditNormal
                    });
                }
            }
            context.SaveChanges();
        }
    }
}
