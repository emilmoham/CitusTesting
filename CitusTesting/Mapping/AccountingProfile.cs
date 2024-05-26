using AutoMapper;
using CitusTesting.Models;

namespace CitusTesting.Mapping
{
    public class AccountingProfiles : Profile
    {
        public AccountingProfiles()
        {
            CreateMap<CreateFacilityDto, Entities.Facility>()
                .ConstructUsing(src => new Entities.Facility(
                        src.Name
                    ));
            
            CreateMap<CreateAccountDto, Entities.Account>()
                .ConstructUsing(src => new Entities.Account(
                        src.Name,
                        src.Number,
                        src.Type,
                        src.FacilityId
                    ));
            
            CreateMap<CreateTransactionDto, Entities.Transaction>()
                .ConstructUsing(src => new Entities.Transaction(
                        src.FacilityId
                    ));

            CreateMap<CreateEntryDto, Entities.Entry>()
                .ConstructUsing(src => new Entities.Entry(
                        src.FacilityId,
                        src.TransactionId,
                        src.AccountId,
                        src.Credit,
                        src.Debit
                    ));
        }
    }
}
