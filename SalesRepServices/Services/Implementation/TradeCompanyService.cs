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
    public class TradeCompanyService : ITradeCompanyService
    {
        private readonly EFContext _context;
        private readonly IMapper _mapper;
        public TradeCompanyService(EFContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OperationStatus> CreateCompany(TradeCompanyModel tradeCompanyModel)
        {
            if (tradeCompanyModel!=null)
            {
                var entity = _mapper.Map<TradeCompanyModel, TradeCompany>(tradeCompanyModel);
                _context.Trades.Add(entity);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true, Message = "200" };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Huston we have a problem!!!" };
        }

        public async Task<OperationStatus> Delete(string title)
        {
            var tdForDelete = await _context.Trades.FirstOrDefaultAsync(x=>x.Title==title);
            if (tdForDelete==null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            _context.Trades.Remove(tdForDelete);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true, Message = "200" };
        }

        public async Task<TradeCompanyModel> GetCompanyByTitle(string title)
        {
            var entity = await _context.Trades
                                .SingleOrDefaultAsync(x=>x.Title==title);
            if (entity==null)
            {
                return new TradeCompanyModel();
            }
            return _mapper.Map<TradeCompany, TradeCompanyModel>(entity);
        }

        public async Task<OperationStatus> Update(TradeCompanyModel tradeCompanyModel)
        {
            var tc = await _context.Trades.FirstOrDefaultAsync(x=>x.TradeCompanyID==tradeCompanyModel.TradeCompanyID);
            if (tc==null)
            {
                return new OperationStatus() { IsSuccess = false, Message = "204" };
            }
            var maptc = _mapper.Map<TradeCompanyModel, TradeCompany>(tradeCompanyModel, tc);
            _context.Trades.Update(maptc);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true, Message = "200" };
        }
    }
}
