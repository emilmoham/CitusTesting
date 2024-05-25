﻿namespace CitusTesting.Services
{
    public interface IEntriesRepository
    {
        Task<IEnumerable<Entities.Entry>> GetEntriesAsync(int? facilityId);
        IAsyncEnumerable<Entities.Entry> GetEntriesAsAsyncEnumerable(int? facilityId);

        Task<Entities.Entry?> GetEntryAsync(int id);

        void AddEntry(Entities.Entry entryToAdd);

        Task<bool> SaveChangesAsync();
    }
}
