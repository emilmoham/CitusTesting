using CitusTesting.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CitusTesting.Services
{
    public class AccountsRepository : IAccountsRepository
    {
        AccountingContext _context;
        
        public AccountsRepository(AccountingContext context)
        {
            _context = context;
        }

        public void AddAccount(Entities.Account accountToAdd)
        {
            if (accountToAdd == null) throw new ArgumentNullException(nameof(accountToAdd));

            _context.Add(accountToAdd);
        }

        public async Task<IEnumerable<Entities.Account>> GetAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public IAsyncEnumerable<Entities.Account> GetAccountsAsAsyncEnumerable()
        {
            return _context.Accounts.AsAsyncEnumerable<Entities.Account>();
        }

        public async Task<Entities.Account?> GetAccountAsync(int id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
