using AutoMapper;
using SalesRepDAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SalesRepDAL.Entities;
using SalesRepServices.Helpers;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesRepServices.Services_Interfaces;
using System;

namespace SalesRepServices.Services.Implementation
{
    public class TradeOrderService : ITradeOrderService
    {
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        private readonly ILogsReport _logs;
        public TradeOrderService(EFContext context, IMapper mapper, ILogsReport logs)
        {
            _context = context;
            _mapper = mapper;
            _logs = logs;
        }
        public async Task<OperationStatus> CreateOrder(TradeOrderModel tradeOrderViewModel)
        {
            if (tradeOrderViewModel != null)
            {
                var entity = _mapper.Map<TradeOrderModel, TradeOrder>(tradeOrderViewModel);
                _context.TradeOrders.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true, Message = "200" };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }

        public async Task<OperationStatus> Delete(int id)
        {
            try
            {
                var toForDelete = await _context.TradeOrders.FirstOrDefaultAsync(x => x.TradeOrderID == id);
                if (toForDelete == null)
                {
                    return new OperationStatus() { IsSuccess = false, Message = "204" };
                }
                _context.TradeOrders.Remove(toForDelete);
                await _context.SaveChangesAsync();
                return new OperationStatus() { Message = "200", IsSuccess = true };
            }
            catch (Exception ex)
            {
                _logs.AnotherExeption(ex);
            }
            return new OperationStatus() { IsSuccess = false, Message = "204" };
        }

        public async Task<List<TradeOrderModel>> GetOrdersOfCustomer(int customerId)
        {
            var ordersOfCustomer = await _context.TradeOrders
                        .Where(x => x.CustomerID == customerId).ToListAsync();
            if (ordersOfCustomer == null)
            {
                //переробити треба 
                return new List<TradeOrderModel>();
            }
            //переробити треба
            var map = new List<TradeOrderModel>();// _mapper.Map<List<TradeOrder>, List<TradeOrderViewModel>>(ordersOfCustomer);
            return map;
        }

        public async Task<OperationStatus> Update(int id, TradeOrderModel tradeOrderViewModel)
        {
            var to = await _context.TradeOrders.FirstOrDefaultAsync(x => x.TradeOrderID == id);
            if (to == null)
            {
                return new OperationStatus() { IsSuccess = true, Message = "204" };
            }
            var mapTO = _mapper.Map<TradeOrderModel, TradeOrder>(tradeOrderViewModel, to);
            _context.TradeOrders.Update(mapTO);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true, Message = "200" };
        }
    }
}
