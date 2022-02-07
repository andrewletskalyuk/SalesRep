using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SalesRepServices.Models
{
    public class SalesRepModel
    {
        [Key]
        public int SaleRepID { get; set; }
        [Required]
        public string FullName { get; set; }
        public int Salary { get; set; }
        public string Itinerary { get; set; }
        public string HomeAddress { get; set; }
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int TradeCompanyID { get; set; }
        List<TradeOrderModel> TradeOrders { get; set; }
    }
}
