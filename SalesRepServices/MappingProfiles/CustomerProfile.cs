using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesRepServices.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDTO, Customer>();
            //CreateMap<Customer, CustomerDTO>();
        }
    }
}
