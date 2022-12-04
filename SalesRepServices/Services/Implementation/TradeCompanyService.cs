using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class TradeCompanyService : ITradeCompanyService
    {
        private readonly ITradeCompanyRepository _tradeCompanyRepository;
        private readonly IMapper _mapper;
        public TradeCompanyService(IMapper mapper, ITradeCompanyRepository tradeCompanyRepository)
        {
            _mapper = mapper;
            _tradeCompanyRepository = tradeCompanyRepository;
        }
        public async Task<OperationStatus> CreateCompany(TradeCompanyModel tradeCompanyModel)
        {
            if (tradeCompanyModel != null)
            {
                var entity = _mapper.Map<TradeCompanyModel, TradeCompany>(tradeCompanyModel);
                if (entity != null)
                {
                    return await _tradeCompanyRepository.CreateCompany(entity);
                }
                return new OperationStatus() { IsSuccess = false, Message = "Company wasn't created" };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }
        public async Task<OperationStatus> Delete(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                return await _tradeCompanyRepository.Delete(title);
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!" };
        }
        public async Task<TradeCompanyModel> GetCompanyByTitle(string title)
        {
            if (!String.IsNullOrEmpty(title))
            {
                var company = await _tradeCompanyRepository.GetCompanyByTitle(title);
                var res = _mapper.Map<TradeCompany, TradeCompanyModel>(company);
                return res; 
            }
            return new TradeCompanyModel();
        }

        public async Task<OperationStatus> Update(TradeCompanyModel tradeCompanyModel)
        {
            if (tradeCompanyModel!=null)
            {
                var map = _mapper.Map<TradeCompanyModel, TradeCompany>(tradeCompanyModel,new TradeCompany());
                return await _tradeCompanyRepository.Update(map);
            }
            return new OperationStatus() { IsSuccess = false, Message="Huston we have a problem!" };
        }
    }
}
