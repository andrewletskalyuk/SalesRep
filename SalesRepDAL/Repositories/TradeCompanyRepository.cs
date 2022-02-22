using SalesRepDAL.Entities;
using SalesRepDAL.Helpers;
using SalesRepDAL.Repositories.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories
{
    public class TradeCompanyRepository : ITradeCompanyRepository
    {
        private readonly EFContext _context;
        public TradeCompanyRepository(EFContext context)
        {
            _context = context;
        }
        public async Task<OperationStatus> CreateCompany(TradeCompany tradeCompany)
        {
            _context.Trades.Add(tradeCompany);
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<OperationStatus> Delete(string title)
        {
            _context.Trades.Remove(_context.Trades.FirstOrDefault(x=>x.Title==title));
            await _context.SaveChangesAsync();
            return new OperationStatus() { IsSuccess = true };
        }
        public async Task<TradeCompany> GetCompanyByTitle(string title)
        {
            var temp = _context.Trades.FirstOrDefault(x => x.Title == title);
            if (temp!=null)
            {
                return temp;
            }
            return new TradeCompany();
        }
        public async Task<OperationStatus> Update(TradeCompany tradeCompany)
        {
            var company = _context.Trades.FirstOrDefault(x => x.TradeCompanyID == tradeCompany.TradeCompanyID);
            if (company != null)
            {
                _context.Trades.Update(company);
                await _context.SaveChangesAsync();
                return new OperationStatus() { IsSuccess = true };
            }
            return new OperationStatus() { IsSuccess = false, Message = "Trade company didn't found!" };
        }
    }
}
