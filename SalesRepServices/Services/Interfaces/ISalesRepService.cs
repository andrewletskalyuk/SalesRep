using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ISalesRepService
    {
        Task<OperationStatus> CreateRep(SalesRepModel salesRepViewModel);
        Task<SalesRepModel> GetByName(string name);
        Task<OperationStatus> Update(int id, SalesRepModel salesRepViewModel);
        Task<OperationStatus> DeleteByName(string name);
    }
}
