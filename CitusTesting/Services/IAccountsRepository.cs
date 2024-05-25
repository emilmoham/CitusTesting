namespace CitusTesting.Services
{
    public interface IAccountsRepository
    {
        IEnumerable<Entities.Account> GetAccounts();
        Task<IEnumerable<Entities.Account>> GetAccountsAsync();
        IAsyncEnumerable<Entities.Account> GetAccountsAsAsyncEnumerable();

        Task<Entities.Account> GetAccountAsync(int id);

        void AddAccount(Entities.Account bookToAdd);

        Task<bool> SaveChangesAsync();
    }
}
