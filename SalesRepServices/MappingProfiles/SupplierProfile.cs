using AutoMapper;
using SalesRepDAL.Entities;
using SalesRepServices.Models;
using System.Collections.Generic;

namespace SalesRepServices.MappingProfiles
{
    public class SupplierProfile: Profile
    {
        public SupplierProfile()
        {
            CreateMap<SupplierViewModel, Supplier>();
            CreateMap<Supplier, SupplierViewModel>();
        }
    }
}
