using AutoMapper;
using SalesRepDAL;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesRep_IntegrationTests.Controllers
{
    public class TradeOrderEndPointTests
    {
        private readonly ITradeOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITradeOrderService _supplierService;
        private readonly EFContext _context;
        private const string DEFAULT_STRING = "DEFAULT_STRING";
        private const string DEFAULT_PHONE = "+380671112233";
        private const string DEFAULT_EMAIL = "defaultemail@gmail.com";
        public TradeOrderEndPointTests()
        {

        }
    }
}
