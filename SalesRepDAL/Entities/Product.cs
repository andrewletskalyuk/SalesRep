using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalesRepDAL.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Title { get; set; }
        [DefaultValue(0)]
        public int QuantityInWarehouse { get; set; }
        [Required]
        public int Price { get; set; }
        [DefaultValue(0)]
        public int TotalSum { get; set; }
        public string Description { get; set; }
        public int SupplierID { get; set; }
    }
}