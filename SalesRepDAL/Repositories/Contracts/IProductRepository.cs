using SalesRepDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalesRepDAL.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<Product> GetById(int id);
    }
}
