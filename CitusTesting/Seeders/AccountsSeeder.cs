using CitusTesting.Models;
using CitusTesting.Seeders.Helpers;

namespace CitusTesting.Seeders
{
    public class AccountsSeeder : IDatabaseSeeder
    {

        private static AccountDefinition[] StandardAccounts = AccountDefinition.StandardAccounts;

        public override void Seed(AccountingContext context)
        {
            Console.WriteLine("Seeding Accounts");
            if (context.Accounts.Count() == context.Facilities.Count() * StandardAccounts.Length)
            {
                Console.WriteLine("Accounts table already seeded");
                return;
            }

            List<Facility> facilities = context.Facilities.ToList();
            foreach (Facility facility in facilities)
            {
                //Console.WriteLine($"{facility.Name} Accounts");
                foreach (AccountDefinition definition in StandardAccounts)
                {
                    //Console.WriteLine($"\t{definition.Name}");
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
