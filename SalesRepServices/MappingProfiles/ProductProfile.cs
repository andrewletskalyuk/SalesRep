using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesRepServices.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductDTO>();
        }
    }
}
