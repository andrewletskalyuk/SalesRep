﻿using SalesRepDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesRepServices.Models
{
    public class SupplierViewModel
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string AdditionalInfo { get; set; }
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
