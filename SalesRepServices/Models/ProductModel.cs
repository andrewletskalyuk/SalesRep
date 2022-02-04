using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SalesRepServices.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        [Required]
        public string Title { get; set; }
        public int QuantityInWarehouse { get; set; }
        public int Price { get; set; }
        public int TotalSum { get; set; }
        public string Description { get; set; }
        [Required]
        public int SupplierID { get; set; }
    }
}
