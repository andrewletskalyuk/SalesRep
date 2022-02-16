using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepDAL.Repositories;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.MappingProfiles;
using SalesRepServices.Models;
using SalesRepServices.Services.Implementation;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_ForSalesRep;
using SalesRepServices.Services_Interfaces;
using SalesRepWebApi;
using System.Configuration;
using Xunit;

namespace SalesRep_IntegrationTests.Controllers
{
    public class SupplierEndPointTests
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISupplierService _supplierService;
        private readonly EFContext _context;
        private const string DEFAULT_STRING = "DEFAULT_STRING";
        private const string DEFAULT_PHONE = "+380671112233";
        private const string DEFAULT_EMAIL = "defaultemail@gmail.com";

        public SupplierEndPointTests()
        {
            var builder = new DbContextOptionsBuilder<EFContext>();
            builder.UseSqlServer($"Data Source=localhost;Database=SalesRepDB;Integrated Security=True; ApplicationIntent=ReadWrite;");
            _context = new EFContext(builder.Options);
            //_context.Database.Migrate();
            _repository = new SupplierRepository(builder.Options, _context);
            _mapper = new MapperConfiguration(config =>
            {
                config.AddMaps(typeof(SupplierProfile));
            }).CreateMapper();
            _supplierService = new SupplierService(_mapper, _repository);
        }

        [Fact]
        public void CreateSupplier_ReturnOperationStatus()
        {
            //Arrange
            var supplierModel = new SupplierModel()
            {
                Title = DEFAULT_STRING,
                Email = DEFAULT_EMAIL,
                Phone = DEFAULT_PHONE,
                IsActive = true,
                Address = DEFAULT_STRING,
                AdditionalInfo = DEFAULT_STRING
            };

            //Act
            var result = _supplierService.CreateSupplier(supplierModel).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void GetSupplierByTitle_ReturnSupplierModel()
        {
            //Arrange
            //Act
            var result = _supplierService.SearchByTitle(DEFAULT_STRING).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(DEFAULT_STRING, result[0].Title);
        }

        [Fact]
        public void GetProductsOfSupplier_ReturnListOfProducts()
        {
            //Arrange
            //Act
            var result = _supplierService.GetProductsOfSupplier(DEFAULT_STRING).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count >= 0);
        }

        [Fact]
        public void UpdateSupplier_ReturnOperationStatus()
        {
            //Arrange 
            var modelForUpdate = new Supplier()
            {
                Title = DEFAULT_STRING,
                AdditionalInfo = DEFAULT_STRING,
                Address = DEFAULT_STRING,
                Email = DEFAULT_EMAIL,
                IsActive = true,
                Phone = DEFAULT_PHONE
            };

            //Act
            var result = _supplierService.Update(_mapper.Map<Supplier,SupplierModel>(modelForUpdate)).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void DeleteSupplier_ReturnOperationStatus()
        {
            //Arrange
            var supplierModel = new SupplierModel()
            {
                Title = DEFAULT_STRING,
                Email = DEFAULT_EMAIL,
                Phone = DEFAULT_PHONE,
                IsActive = true,
                Address = DEFAULT_STRING,
                AdditionalInfo = DEFAULT_STRING
            };
            var resultCreatedForDelete = _repository.CreateSupplier(_mapper.Map<SupplierModel, Supplier>(supplierModel)).GetAwaiter().GetResult();

            //Act
            var result = _supplierService.Delete(DEFAULT_STRING).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(resultCreatedForDelete);
            Assert.NotNull(result);
            Assert.Equal(resultCreatedForDelete.IsSuccess, result.IsSuccess);
        }

        [Fact]
        public void SearchByTitle_ReturnSupplierModel()
        {
            //Arrange
            var supplierModel = new SupplierModel()
            {
                Title = DEFAULT_STRING,
                Email = DEFAULT_EMAIL,
                Phone = DEFAULT_PHONE,
                IsActive = true,
                Address = DEFAULT_STRING,
                AdditionalInfo = DEFAULT_STRING
            };
            var resultCreatedForSearch = _repository.CreateSupplier(_mapper.Map<SupplierModel, Supplier>(supplierModel)).GetAwaiter().GetResult();

            //Act
            var result = _supplierService.SearchByTitle(DEFAULT_STRING).GetAwaiter().GetResult();

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(resultCreatedForSearch);
            Assert.True(result.Count >= 0);
        }
    }
}
