using SalesRepDAL.Helpers;
using SalesRepServices.Models;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ISalesRepService
    {
        Task<OperationStatus> CreateRep(SalesRepModel salesRepModel);
        Task<SalesRepModel> GetByName(string name);
        Task<OperationStatus> Update(SalesRepModel salesRepModel);
        Task<OperationStatus> DeleteByName(string name);
    }
}
