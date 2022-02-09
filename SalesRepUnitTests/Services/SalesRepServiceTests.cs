using AutoMapper;
using Moq;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.MappingProfiles;
using SalesRepServices.Models;
using SalesRepServices.Services.Implementation;
using SalesRepServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SalesRepUnitTests.Services
{
    public class SalesRepServiceTests
    {
        private readonly Mock<ISalesRepRepository> _repository;
        private readonly IMapper _mapper;
        private readonly ISalesRepService _salesRepService;
        private const string DEFAULT_STRING = "DEFAULT_STRING";
        private const string DEFAULT_PHONE = "38 067 111 22 33";
        private const int DEFAULT_INT = 100;
        private const string DEFAULT_EMAIL = "andrewletskalyuk@gmail.com";

        public SalesRepServiceTests()
        {
            _repository = new Mock<ISalesRepRepository>();
            _mapper = new MapperConfiguration(config=> {
                config.AddMaps(typeof(SalesRepProfile));
            }).CreateMapper();
            _salesRepService = new SalesRepService(_mapper,_repository.Object);
        }

        [Fact]
        public void CreateSalesRep_ResultOperationStatus()
        {
            //Arrange
            var salesRepModel = new SalesRepModel()
            {
                FullName = DEFAULT_STRING,
                HomeAddress = DEFAULT_STRING,
                Phone = DEFAULT_PHONE,
                Salary = DEFAULT_INT,
                IsActive = true,
                Itinerary = DEFAULT_STRING,
                Email = DEFAULT_EMAIL
            };
            _repository.Setup(x => x.CreateRep(It.IsAny<SaleRep>())).ReturnsAsync(new OperationStatus() { IsSuccess = true });

            //Act
            var result = _salesRepService.CreateRep(salesRepModel).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }
    }
}
