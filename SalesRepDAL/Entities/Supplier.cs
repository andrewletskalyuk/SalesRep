using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalesRepDAL.Entities
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        public string Address { get; set; }
        [Required]
        [StringLength(22, MinimumLength = 10)]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }
        public string AdditionalInfo { get; set; }
        public List<TradeCompany_Supplier> TradeCompany_Suppliers { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
