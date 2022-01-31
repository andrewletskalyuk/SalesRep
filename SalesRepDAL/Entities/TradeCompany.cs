using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesRepDAL.Entities
{
    public class TradeCompany
    {
        [Key]
        public int TradeCompanyID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [StringLength(22, MinimumLength = 10)]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Owner { get; set; }
        public string TaxSystem { get; set; }
        public List<TradeCompany_Supplier> TradeCompany_Suppliers { get; set; }
        public List<SaleRep> SaleReps { get; set; } = new List<SaleRep>();
    }
}