using SalesRepDAL.Entities;

namespace SalesRepServices.Services_Interfaces
{
    public interface IJwtTokenService
    {
        string CreateToken(DbUser user);
    }
}
