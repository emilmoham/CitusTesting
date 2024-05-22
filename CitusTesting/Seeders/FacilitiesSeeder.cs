using CitusTesting.Models;

namespace CitusTesting.Seeders
{
    public class FacilitiesSeeder : IDatabaseSeeder
    {
        private string[] cities =
        {
            "Philadelphia",
            "New York",
            "Boston",
            "Washington D.C.",
            "Phoenix",
            "Cincinnati",
            "Scranton",
            "Cleveland",
            "Jackson",
            "Trenton"
        };

        public override void Seed(AccountingContext context)
        {
            int currentFacilitiesCount = context.Facilities.Count();
            if (cities.Length == currentFacilitiesCount)
            {
                Console.WriteLine("Facilities table already seeded");
                return;
            }

            foreach (string city in cities)
            {
                Console.WriteLine($"New facility: {city}");
                context.Add(new Facility() { Name=city });
            }
            context.SaveChanges();
        }
    }
}
