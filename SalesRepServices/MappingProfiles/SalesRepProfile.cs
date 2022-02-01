using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepServices.Models;

namespace SalesRepServices.MappingProfiles
{
    public class SalesRepProfile : Profile
    {
        public SalesRepProfile()
        {
            CreateMap<SaleRep, SalesRepViewModel>();
            CreateMap<SalesRepViewModel, SaleRep>();
        }
    }
}
