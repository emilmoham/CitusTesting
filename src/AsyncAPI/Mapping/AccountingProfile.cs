using AutoMapper;
using AsyncAPI.Models.Args;

namespace AsyncAPI.Mapping
{
    public class AccountingProfiles : Profile
    {
        public AccountingProfiles()
        {
            CreateMap<CreateFacility, Entities.Facility>()
                .ConstructUsing(src => new Entities.Facility(
                        src.Name
                    ));
            
            CreateMap<CreateTransaction, Entities.Transaction>()
                .ConstructUsing(src => new Entities.Transaction(
                        src.FacilityId
                    ));
        }
    }
}
