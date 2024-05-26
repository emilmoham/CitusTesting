using CitusTesting.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CitusTesting.Services
{
    public class FacilitiesRepository : IFacilitiesRepository
    {
        private AccountingContext _context;
        
        public FacilitiesRepository(AccountingContext context)
        {
            _context = context;
        }

        public void AddFacility(Entities.Facility facilityToAdd)
        {
            if (facilityToAdd == null) throw new ArgumentNullException(nameof(facilityToAdd));

            _context.Add(facilityToAdd);
        }

        public async Task<IEnumerable<Entities.Facility>> GetFacilitiesAsync()
        {
            return await _context.Facilities.ToListAsync();
        }

        public IAsyncEnumerable<Entities.Facility> GetFacilitiesAsAsyncEnumerable()
        {
            return _context.Facilities.AsAsyncEnumerable<Entities.Facility>();
        }

        public async Task<Entities.Facility?> GetFacilityAsync(int id)
        {
            return await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
