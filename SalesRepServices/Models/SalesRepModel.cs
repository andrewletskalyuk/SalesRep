using System;
using System.Collections.Generic;
using System.Text;

namespace SalesRepServices.Models
{
    public class SalesRepModel
    {
        public string FullName { get; set; }
        public int Salary { get; set; }
        public string Itinerary { get; set; }
        public string HomeAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public int TradeCompanyID { get; set; }
    }
}
