using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using SalesRepServices.Services_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        private readonly ILogsReport _logsReport;
        public SupplierService(IMapper mapper, ILogsReport logsReport, ISupplierRepository supplierRepository)
        {
            _mapper = mapper;
            _logsReport = logsReport;
            _supplierRepository = supplierRepository;
        }
        public async Task<OperationStatus> CreateSupplier(SupplierModel supplierModel)
        {
            if (supplierModel != null)
            {
                return await _supplierRepository.CreateSupplier(_mapper.Map<SupplierModel, Supplier>(supplierModel));
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }

        public async Task<OperationStatus> Delete(string title)
        {
            if (!String.IsNullOrEmpty(title))
            {
                return await _supplierRepository.Delete(title);
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }

        public async Task<SupplierModel> GetByTitle(string title)
        {
            if (!String.IsNullOrEmpty(title))
            {
                var res = await _supplierRepository.GetByTitle(title);
                var map = _mapper.Map<Supplier, SupplierModel>(res);
                return map;
            }
            return new SupplierModel();
        }

        public async Task<IList<ProductModel>> GetProductsOfSupplier(string supplierTitle)
        {
            var listOfProduct = new List<ProductModel>();
            if (!String.IsNullOrEmpty(supplierTitle))
            {
                var products = await _supplierRepository.GetProductsOfSupplier(supplierTitle);
                if (products.ToList().Count > 0)
                {
                    try
                    {
                        foreach (var Product in products) //ToList
                        {
                            listOfProduct.Add(_mapper.Map<Product, ProductModel>(Product));
                        }
                        return listOfProduct;
                    }
                    catch (Exception ex)
                    {
                        _logsReport.AnotherExeption(ex);
                    }
                }
            }
            return listOfProduct;
        }

        public async Task<List<SupplierModel>> SearchByTitle(string text)
        {
            var res = new List<SupplierModel>();
            if (!String.IsNullOrEmpty(text))
            {
                var suppliers = _supplierRepository.SearchByTitle(text);
                if (suppliers != null)
                {
                    foreach (var Supplier in suppliers.Result)
                    {
                        res.Add(_mapper.Map<Supplier, SupplierModel>(Supplier));
                    }
                    return res;
                }
            }
            return res;
        }

        public async Task<OperationStatus> Update(SupplierModel supplierModel)
        {
            if (supplierModel != null)
            {
                return await _supplierRepository.Update(_mapper.Map<SupplierModel, Supplier>(supplierModel));
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!" };
        }
    }
}
