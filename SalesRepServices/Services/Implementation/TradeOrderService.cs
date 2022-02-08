﻿using AutoMapper;
using SalesRepDAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SalesRepDAL.Entities;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesRepServices.Services_Interfaces;
using System;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;

namespace SalesRepServices.Services.Implementation
{
    public class TradeOrderService : ITradeOrderService
    {
        private readonly IMapper _mapper;
        private readonly ILogsReport _logsReport;
        private readonly ITradeOrderRepository _tradeOrderRepository;
        public TradeOrderService(IMapper mapper, ILogsReport logsReport, ITradeOrderRepository tradeOrderRepository)
        {
            _mapper = mapper;
            _logsReport = logsReport;
            _tradeOrderRepository = tradeOrderRepository;
        }
        public async Task<OperationStatus> CreateOrder(TradeOrderModel tradeOrderModel)
        {
            if (tradeOrderModel != null)
            {
                var entity = _mapper.Map<TradeOrderModel, TradeOrder>(tradeOrderModel);
                var res = await _tradeOrderRepository.CreateOrder(entity);
                return res;
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }

        public async Task<OperationStatus> Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    return await _tradeOrderRepository.Delete(id);
                }
            }
            catch (Exception ex)
            {
                _logsReport.AnotherExeption(ex);
            }
            return new OperationStatus() { IsSuccess = false, Message = "204" };
        }

        public async Task<List<TradeOrderModel>> GetOrdersOfCustomer(int customerId)
        {
            //IQueryable<TradeOrder> ordersOfCustomer = _context.TradeOrders
            //                            .Where(x => x.CustomerID == customerId);

            //working variant - same result!!!
            //IQueryable<TradeOrder> resZ = _context.Customers.SelectMany(x => x.TradeOrders).Where(z=>z.CustomerID==customerId);

            if (customerId != 0)
            {
                var orders = _tradeOrderRepository.GetOrdersOfCustomer(customerId);
                List<TradeOrderModel> res = new List<TradeOrderModel>();
                if (orders == null)
                {
                    return res;
                }
                try
                {
                    foreach (var TradeOrder in orders.Result)
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
            return new List<TradeOrderModel>();
        }

        public async Task<OperationStatus> Update(TradeOrderModel tradeOrderModel)
        {
            if (tradeOrderModel != null)
            {
                var temp = new TradeOrder();
                var map = _mapper.Map<TradeOrderModel, TradeOrder>(tradeOrderModel, temp);
                return await _tradeOrderRepository.Update(map);
            }
            //var to = await _context.TradeOrders.FirstOrDefaultAsync(x => x.TradeOrderID == tradeOrderModel.TradeOrderID);
            //if (to == null)
            //{
            //    return new OperationStatus() { IsSuccess = true, Message = "204" };
            //}
            //var mapTO = _mapper.Map<TradeOrderModel, TradeOrder>(tradeOrderModel, to);
            //_context.TradeOrders.Update(mapTO);
            //await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!" };
        }
    }
}
