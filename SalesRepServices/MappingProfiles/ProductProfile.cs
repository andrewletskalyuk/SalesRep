using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepServices.Models;

namespace SalesRepServices.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();
        }
    }
}
