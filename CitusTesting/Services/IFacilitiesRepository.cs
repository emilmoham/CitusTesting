﻿namespace CitusTesting.Services
{
    public interface IFacilitiesRepository
    {
        Task<IEnumerable<Entities.Facility>> GetFacilitiesAsync();
        IAsyncEnumerable<Entities.Facility> GetFacilitiesAsAsyncEnumerable();

        Task<Entities.Facility?> GetFacilityAsync(int id);

        void AddFacility(Entities.Facility facilityToAdd);

        Task<bool> SaveChangesAsync();
    }
}
