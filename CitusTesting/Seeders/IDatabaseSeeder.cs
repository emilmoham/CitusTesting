using CitusTesting.DbContexts;

namespace CitusTesting.Seeders
{
    public abstract class IDatabaseSeeder
    {
        public abstract void Seed(AccountingContext context);
    }
}
