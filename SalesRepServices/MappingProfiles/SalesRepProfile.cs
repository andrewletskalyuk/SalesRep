using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepServices.Models;

namespace SalesRepServices.MappingProfiles
{
    public class SalesRepProfile : Profile
    {
        public SalesRepProfile()
        {
            CreateMap<SaleRep, SalesRepModel>();
            CreateMap<SalesRepModel, SaleRep>();
        }
    }
}
