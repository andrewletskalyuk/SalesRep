using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class SalesRepService : ISalesRepService
    {
        private readonly IMapper _mapper;
        private readonly ISalesRepRepository _salesRepRepository;
        public SalesRepService(IMapper mapper, ISalesRepRepository salesRepRepository)
        {
            _salesRepRepository = salesRepRepository;
            _mapper = mapper;
        }
        public async Task<OperationStatus> CreateRep(SalesRepModel salesRepModel)
        {
            if (salesRepModel != null)
            {
                var entity = _mapper.Map<SalesRepModel, SaleRep>(salesRepModel);
                var res = await _salesRepRepository.CreateRep(entity);
                return res;
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }

        public async Task<OperationStatus> DeleteByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var res = await _salesRepRepository.DeleteByName(name);
                return res;
            }
            return new OperationStatus() { IsSuccess = false, Message = "SaleRep wasn't found!" };
        }

        public async Task<SalesRepModel> GetByName(string name)
        {
            var entity = await _salesRepRepository.GetByName(name);
            if (entity == null)
            {
                return new SalesRepModel();
            }
            return _mapper.Map<SaleRep, SalesRepModel>(entity);
        }

        public async Task<IList<CustomerOfSalesRepModel>> GetCustomersOfSalesRep(string nameSalesRep)
        {
            var result = await _salesRepRepository.GetCustomersOfSalesRep(nameSalesRep);
            if (result!=null)
            {
                return result;
            }
            var list = new List<CustomerOfSalesRepModel>();
            return list;
        }

        public async Task<OperationStatus> Update(SalesRepModel salesRepModel)
        {
            if (salesRepModel != null)
            {
                return await _salesRepRepository
                        .Update(_mapper.Map<SalesRepModel, SaleRep>(salesRepModel));
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!" };
        }
    }
}
