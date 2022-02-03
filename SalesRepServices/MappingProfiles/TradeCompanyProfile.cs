using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepServices.Models;

namespace SalesRepServices.MappingProfiles
{
    public class TradeCompanyProfile:Profile
    {
        public TradeCompanyProfile()
        {
            CreateMap<TradeCompany, TradeCompanyModel>();
            CreateMap<TradeCompanyModel, TradeCompany>();
        }
    }
}
