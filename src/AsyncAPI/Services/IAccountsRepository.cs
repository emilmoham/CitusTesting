namespace AsyncAPI.Services
{
    public interface IAccountsRepository
    {
        Task<IEnumerable<Entities.Account>> GetAccountsAsync(int? id = null);
        IAsyncEnumerable<Entities.Account> GetAccountsAsAsyncEnumerable();

        Task<Entities.Account?> GetAccountAsync(int id);

        void AddAccount(Entities.Account accountToAdd);

        void AddAccounts(Entities.Account[] accountToAdd);

        void UpdateAccount(Entities.Account accountToUpdate);

        void UpdateAccounts(Entities.Account[] accountsToUpdate);

        Task<bool> SaveChangesAsync();
    }
}
