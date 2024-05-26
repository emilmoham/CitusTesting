namespace CitusTesting.Services
{
    public interface ITransactionsRepository
    {
        Task<IEnumerable<Entities.Transaction>> GetTransactionsAsync(int? facilityId);
        IAsyncEnumerable<Entities.Transaction> GetTransactionsAsAsyncEnumerable(int? facilityId);

        Task<Entities.Transaction?> GetTransactionAsync(int id);

        void AddTransaction(Entities.Transaction transactionToAdd);

        Task<bool> SaveChangesAsync();
    }
}
