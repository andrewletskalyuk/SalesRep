using System;
using System.Collections.Generic;
using System.Text;

namespace SalesRepServices.Models
{
    public class ProductViewModel
    {
        public string Title { get; set; }
        public int QuantityInWarehouse { get; set; }
        public int Price { get; set; }
        public int TotalSum { get; set; }
        public string Description { get; set; }
        public int SupplierID { get; set; }
    }
}
