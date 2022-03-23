using AutoMapper;
using MassTransit;
using SalesRepDAL.Entities;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SalesRepWebApi.Consumers
{
    public class TradeOrderConsumer : IConsumer<TradeOrder>
    {
        private readonly ITradeOrderService _tradeOrderService;
        private readonly IMapper _mapper;
        public TradeOrderConsumer(ITradeOrderService tradeOrderService, IMapper mapper)
        {
            _tradeOrderService = tradeOrderService;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<TradeOrder> tradeOrder)
        {
            var data = tradeOrder.Message;
            //validation data
            if (data != null && data.CustomerID != 0)
            {
                try
                {
                    var tradeOrderModel = _mapper.Map<TradeOrder, TradeOrderModel>(data);
                    await _tradeOrderService.CreateOrder(tradeOrderModel);
                    //in this case we're saved data to DB
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public async Task Consume(ConsumeContext<TradeOrderID> tradeOrder)
        {
            var data = tradeOrder.Message;
            //validation data
            if (data != null && data.Id != 0)
            {
                try
                {
                    await _tradeOrderService.Delete(data.Id);
                    //in this case we're saved data to DB
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
