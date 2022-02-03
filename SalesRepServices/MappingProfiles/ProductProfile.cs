using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepServices.Models;

namespace SalesRepServices.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductModel, Product>();
            CreateMap<Product, ProductModel>();
        }
    }
}
