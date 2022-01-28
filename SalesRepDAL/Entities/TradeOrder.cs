using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesRepDAL.Entities
{
    public class TradeOrder
    {
        [Key]
        public int TradeOrderID { get; set; }
        [Required]
        public int SumOfOrder { get; set; }
        [Required]
        public string DeliveryAddress { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AdditionalInfo { get; set; }
        public int CustomerID { get; set; }
        public int SalesRepID { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
