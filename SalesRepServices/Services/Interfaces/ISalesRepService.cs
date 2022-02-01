using SalesRepServices.Helpers;
using SalesRepServices.Models;
using System.Threading.Tasks;

namespace SalesRepServices.Services.Interfaces
{
    public interface ISalesRepService
    {
        Task<OperationStatus> CreateRep(SalesRepViewModel salesRepViewModel);
        Task<SalesRepViewModel> GetByName(string name);
        Task<OperationStatus> Update(int id, SalesRepViewModel salesRepViewModel);
        Task<OperationStatus> DeleteByName(string name);
    }
}
