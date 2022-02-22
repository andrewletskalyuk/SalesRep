using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesRepServices.Models
{
    public class TradeOrderModel
    {
        [Key]
        public int TradeOrderID { get; set; }
        [Required]
        public int SumOfOrder { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AdditionalInfo { get; set; }
        public int CustomerID { get; set; }
        public int SalesRepID { get; set; }
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
