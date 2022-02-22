using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalesRepDAL.Entities
{
    public class Customer
    {
        [Key]
        public int CusomerID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        public string Address { get; set; } = "default value";
        [DefaultValue(false)]
        public bool IsActive { get; set; }
        [Required]
        public string Phone { get; set; }
        public string AdditionalInfo { get; set; }
        public List<TradeOrder> TradeOrders { get; set; }
    }
}
