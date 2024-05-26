using CitusTesting.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CitusTesting.Services
{
    public class EntriesRepository : IEntriesRepository
    {
        private AccountingContext _context;

        public EntriesRepository(AccountingContext context)
        {
            _context = context;
        }

        public void AddEntry(Entities.Entry EntryToAdd)
        {
            if (EntryToAdd == null) throw new ArgumentNullException(nameof(EntryToAdd));

            _context.Entries.Add(EntryToAdd);
        }

        public async Task<IEnumerable<Entities.Entry>> GetEntriesAsync(int? facilityId)
        {
            if (facilityId != null)
                return await _context.Entries.Where(t => t.FacilityId == facilityId).ToListAsync();
            return await _context.Entries.ToListAsync();
        }

        public IAsyncEnumerable<Entities.Entry> GetEntriesAsAsyncEnumerable(int? facilityId)
        {
            if (facilityId != null)
                return _context.Entries.Where(t => t.FacilityId == facilityId).AsAsyncEnumerable();
            return _context.Entries.AsAsyncEnumerable();
        }

        public async Task<Entities.Entry?> GetEntryAsync(int id)
        {
            return await _context.Entries.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
