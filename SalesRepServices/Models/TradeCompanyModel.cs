using System;
using System.Collections.Generic;
using System.Text;

namespace SalesRepServices.Models
{
    public class TradeCompanyModel
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Owner { get; set; }
        public string TaxSystem { get; set; }
        public List<SalesRepModel> SaleReps { get; set; } = new List<SalesRepModel>();
    }
}
