using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesRepDAL;
using SalesRepDAL.Entities;
using SalesRepServices.Helpers;
using SalesRepServices.Models;
using SalesRepServices.Services.Interfaces;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Implementation
{
    public class SalesRepService : ISalesRepService
    {
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        public SalesRepService(EFContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OperationStatus> CreateRep(SalesRepModel salesRepModel)
        {
            if (salesRepModel!= null)
            {
                var entity = _mapper.Map<SalesRepModel, SaleRep>(salesRepModel);
                _context.SaleRep.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true, Message = "200" };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }

        public async Task<OperationStatus> DeleteByName(string name)
        {
            var salesRepForDelete = await _context.SaleRep.FirstOrDefaultAsync(x => x.FullName == name);
            if (salesRepForDelete == null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            _context.SaleRep.Remove(salesRepForDelete);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true, Message = "200" };
        }

        public async Task<SalesRepModel> GetByName(string name)
        {
            var entity = await _context.SaleRep
                        .SingleOrDefaultAsync(x => x.FullName == name);
            if (entity==null)
            {
                return new SalesRepModel();
            }
            return _mapper.Map<SaleRep, SalesRepModel>(entity);

        }

        public async Task<OperationStatus> Update(SalesRepModel salesRepModel)
        {
            var repUpdate = await _context.SaleRep.FirstOrDefaultAsync(x=>x.SaleRepID == salesRepModel.SaleRepID);
            if (repUpdate==null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            var map = _mapper.Map<SalesRepModel, SaleRep>(salesRepModel, repUpdate);
            _context.SaleRep.Update(map);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true, Message = "200" };
        }
    }
}
