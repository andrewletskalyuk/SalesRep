using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalesRepDAL.Entities
{
    public class SaleRep
    {
        [Key]
        public int SaleRepID { get; set; }
        [Required]
        public string FullName { get; set; }
        [DefaultValue(0)]
        public int Salary { get; set; }
        [Required]
        public string Itinerary { get; set; } = "Default";
        public string HomeAddress { get; set; }
        [Required]
        [StringLength(22, MinimumLength = 10)]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        public int TradeCompanyID { get; set; }
        public List<TradeOrder> TradeOrders { get; set; } = new List<TradeOrder>();
    }
}