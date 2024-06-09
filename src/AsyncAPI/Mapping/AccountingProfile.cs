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
            
            CreateMap<CreateAccount, Entities.Account>()
                .ConstructUsing(src => new Entities.Account(
                        src.Name,
                        src.Number,
                        src.Type,
                        src.FacilityId,
                        src.Balance
                    ));
            
            CreateMap<CreateTransaction, Entities.Transaction>()
                .ConstructUsing(src => new Entities.Transaction(
                        src.FacilityId
                    ));

            CreateMap<CreateEntry, Entities.Entry>()
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
