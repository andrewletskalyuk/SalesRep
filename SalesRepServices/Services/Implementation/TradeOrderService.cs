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
        private readonly ILogsReport _logsReport;
        public TradeOrderService(EFContext context, IMapper mapper, ILogsReport logsReport)
        {
            _context = context;
            _mapper = mapper;
            _logsReport = logsReport;
        }
        public async Task<OperationStatus> CreateOrder(TradeOrderModel tradeOrderModel)
        {
            if (tradeOrderModel != null)
            {
                var entity = _mapper.Map<TradeOrderModel, TradeOrder>(tradeOrderModel);
                _context.TradeOrders.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true };
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
                _logsReport.AnotherExeption(ex);
            }
            return new OperationStatus() { IsSuccess = false, Message = "204" };
        }

        public async Task<List<TradeOrderModel>> GetOrdersOfCustomer(int customerId)
        {
            IQueryable<TradeOrder> ordersOfCustomer = _context.TradeOrders
                                        .Where(x => x.CustomerID == customerId);

            //working variant - same result!!!
            //IQueryable<TradeOrder> resZ = _context.Customers.SelectMany(x => x.TradeOrders).Where(z=>z.CustomerID==customerId);
            List<TradeOrderModel> res = new List<TradeOrderModel>();
            if (ordersOfCustomer == null)
            {
                return res;
            }
            try
            {
                foreach (var TradeOrder in ordersOfCustomer)
                {
                    res.Add(_mapper.Map<TradeOrder, TradeOrderModel>(TradeOrder));
                }
            }
            catch (Exception ex)
            {
                _logsReport.AnotherExeption(ex);
            }
            return res;
        }

        public async Task<OperationStatus> Update(TradeOrderModel tradeOrderModel)
        {
            var to = await _context.TradeOrders.FirstOrDefaultAsync(x => x.TradeOrderID == tradeOrderModel.TradeOrderID);
            if (to == null)
            {
                return new OperationStatus() { IsSuccess = true, Message = "204" };
            }
            var mapTO = _mapper.Map<TradeOrderModel, TradeOrder>(tradeOrderModel, to);
            _context.TradeOrders.Update(mapTO);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
    }
}
