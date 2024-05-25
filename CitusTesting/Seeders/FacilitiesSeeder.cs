using CitusTesting.DbContexts;
using CitusTesting.Models;
using CitusTesting.Seeders.Helpers;

namespace CitusTesting.Seeders
{
    public class FacilitiesSeeder : IDatabaseSeeder
    {
        private static string[] StandardFacilities = FacilityDefinitions.StandardFacilities;

        public override void Seed(AccountingContext context)
        {
            Console.WriteLine("Seeding Facilities");
            int currentFacilitiesCount = context.Facilities.Count();
            if (StandardFacilities.Length == currentFacilitiesCount)
            {
                Console.WriteLine("Facilities table already seeded");
                return;
            }

            foreach (string city in StandardFacilities)
            {
                //Console.WriteLine($"New facility: {city}");
                context.Add(new Facility() { Name=city });
            }
            context.SaveChanges();
        }
    }
}
