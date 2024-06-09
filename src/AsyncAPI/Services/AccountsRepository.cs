using AsyncAPI.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AsyncAPI.Services
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

        public void AddAccounts(Entities.Account[] accountsToAdd) {
            if (accountsToAdd == null || accountsToAdd.Length == 0) throw new ArgumentNullException(nameof(accountsToAdd));

            _context.AddRange(accountsToAdd);
        }

        public async Task<IEnumerable<Entities.Account>> GetAccountsAsync(int? id = null)
        {
            if (id != null)
                return await _context.Accounts.Where(a => a.FacilityId == id).ToListAsync();
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
