using AutoMapper;
using CitusTesting.Models;
using Microsoft.AspNetCore.Routing.Constraints;

namespace CitusTesting.Mapping
{
    public class FacilitiesProfile : Profile
    {
        public FacilitiesProfile()
        {
            CreateMap<CreateFacilityDto, Entities.Facility>()
                .ConstructUsing(src => new Entities.Facility(0, src.Name));
        }
    }
}
