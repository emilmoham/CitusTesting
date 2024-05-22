using CitusTesting.Models;

namespace CitusTesting.Seeders
{
    public abstract class IDatabaseSeeder
    {
        public abstract void Seed(AccountingContext context);
    }
}
