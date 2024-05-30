namespace AsyncAPI.Services
{
    public interface IAccountsRepository
    {
        Task<IEnumerable<Entities.Account>> GetAccountsAsync();
        IAsyncEnumerable<Entities.Account> GetAccountsAsAsyncEnumerable();

        Task<Entities.Account?> GetAccountAsync(int id);

        void AddAccount(Entities.Account accountToAdd);

        Task<bool> SaveChangesAsync();
    }
}
