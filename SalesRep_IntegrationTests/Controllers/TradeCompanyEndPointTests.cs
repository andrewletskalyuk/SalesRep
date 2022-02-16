using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.MappingProfiles;
using SalesRepServices.Models;
using SalesRepServices.Services.Implementation;
using SalesRepServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SalesRep_IntegrationTests.Controllers
{
    public class TradeCompanyEndPointTests
    {
        private readonly ITradeCompanyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITradeCompanyService _tradeCompanyService;
        private readonly EFContext _context;
        private const string DEFAULT_STRING = "DEFAULT_STRING";
        private const string DEFAULT_PHONE = "+380671112233";
        private const string DEFAULT_EMAIL = "defaultemail@gmail.com";
        private const int DEFAULT_INT = 100;
        public TradeCompanyEndPointTests()
        {
            var builder = new DbContextOptionsBuilder<EFContext>();
            builder.UseSqlServer($"Data Source=localhost;Database=SalesRepDB;Integrated Security=True; ApplicationIntent=ReadWrite;");
            _context = new EFContext(builder.Options);
            //_context.Database.Migrate();
            _repository = new TradeCompanyRepository(_context);
            _mapper = new MapperConfiguration(config =>
            {
                config.AddMaps(typeof(TradeCompanyProfile));
            }).CreateMapper();
            _tradeCompanyService = new TradeCompanyService(_mapper, _repository);
        }

        [Fact]
        public void CreateCompanyTest_ReturnOperationStatus()
        {
            //Arrange
            var companyModel = new TradeCompanyModel()
            {
                Title = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                Email = DEFAULT_EMAIL,
                Address = DEFAULT_STRING,
                Owner = DEFAULT_STRING,
                TaxSystem = DEFAULT_STRING,
                SaleReps = new List<SalesRepModel>()
            };
            
            //Act
            var result = _tradeCompanyService.CreateCompany(companyModel).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void DeleteCompanyTest_ReturnOperationStatus()
        {
            //Arrange
            var companyModel = new TradeCompanyModel()
            {
                Title = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                Email = DEFAULT_EMAIL,
                Address = DEFAULT_STRING,
                Owner = DEFAULT_STRING,
                TaxSystem = DEFAULT_STRING,
                SaleReps = new List<SalesRepModel>()
            };
            var resultCreteCompany = _tradeCompanyService.CreateCompany(companyModel).GetAwaiter().GetResult();

            //Act
            var result = _tradeCompanyService.Delete(DEFAULT_STRING).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(resultCreteCompany);
            Assert.NotNull(result);
            Assert.Equal(result.IsSuccess, resultCreteCompany.IsSuccess);
        }

        [Fact]
        public void GetCompanyByTitleTest_ReturnCompanyModel()
        {
            //Arrange
            var companyModel = new TradeCompanyModel()
            {
                Title = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                Email = DEFAULT_EMAIL,
                Address = DEFAULT_STRING,
                Owner = DEFAULT_STRING,
                TaxSystem = DEFAULT_STRING,
                SaleReps = new List<SalesRepModel>()
            };
            var resultCreteCompany = _repository.CreateCompany(_mapper
                        .Map<TradeCompanyModel,TradeCompany>(companyModel))
                        .GetAwaiter().GetResult();

            //Act
            var resultModel = _tradeCompanyService.GetCompanyByTitle(DEFAULT_STRING)
                    .GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(resultCreteCompany);
            Assert.NotNull(resultModel);
            Assert.True(resultModel.Title==DEFAULT_STRING);
        }

        [Fact]
        public void UpdateCompanyModel_ReturnOperationStatus()
        {
            //Arrange
            var companyModel = new TradeCompany()
            {
                Title = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                Email = DEFAULT_EMAIL,
                Address = DEFAULT_STRING,
                Owner = DEFAULT_STRING,
                TaxSystem = DEFAULT_STRING,
                SaleReps = new List<SaleRep>()
            };
            var resultCreteCompany = _repository.CreateCompany(companyModel)
                        .GetAwaiter().GetResult();

            //Act
            var result = _tradeCompanyService
                .Update(_mapper.Map<TradeCompany,TradeCompanyModel>(companyModel))
                .GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            var os = new OperationStatus() { IsSuccess = true };
            Assert.Equal(result.IsSuccess, os.IsSuccess);
        }
    }
}
