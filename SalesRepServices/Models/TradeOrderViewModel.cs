using System;
using System.Collections.Generic;

namespace SalesRepServices.Models
{
    public class TradeOrderViewModel
    {
        public int SumOfOrder { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AdditionalInfo { get; set; }
        public int CustomerID { get; set; }
        public int SalesRepID { get; set; }
        public List<ProductViewModel> Products { get; set; } = new List<ProductViewModel>();
    }
}
