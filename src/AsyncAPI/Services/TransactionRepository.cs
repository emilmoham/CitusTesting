using AsyncAPI.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AsyncAPI.Services
{
    public class TransactionRepository : ITransactionsRepository
    {
        private AccountingContext _context;

        public TransactionRepository(AccountingContext context)
        {
            _context = context;
        }

        public void AddTransaction(Entities.Transaction transactionToAdd)
        {
            if (transactionToAdd == null) throw new ArgumentNullException(nameof(transactionToAdd));

            _context.Transactions.Add(transactionToAdd);
        }

        public async Task<IEnumerable<Entities.Transaction>> GetTransactionsAsync(int? facilityId) {
            if (facilityId != null)
                return await _context.Transactions.Where(t => t.FacilityId == facilityId).ToListAsync(); 
            return await _context.Transactions.ToListAsync();
        }

        public IAsyncEnumerable<Entities.Transaction> GetTransactionsAsAsyncEnumerable(int? facilityId)
        {
            if (facilityId != null)
                return _context.Transactions.Where(t => t.FacilityId == facilityId).AsAsyncEnumerable();
            return _context.Transactions.AsAsyncEnumerable();
        }

        public async Task<Entities.Transaction?> GetTransactionAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
