using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepServices.Models;
using System.Collections.Generic;

namespace SalesRepServices.MappingProfiles
{
    public class TradeOrderProfile : Profile
    {
        public TradeOrderProfile()
        {
            CreateMap<TradeOrder, TradeOrderViewModel>();
            CreateMap<TradeOrderViewModel, TradeOrder>();
            CreateMap<List<TradeOrder>, List<TradeOrderViewModel>>();
            CreateMap<List<TradeOrderViewModel>, List<TradeOrder>>();
        }
    }
}
