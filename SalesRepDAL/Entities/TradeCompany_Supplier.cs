using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SalesRepDAL.Entities
{
    public class TradeCompany_Supplier
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierID { get; set; }
        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }
        [ForeignKey("TradeCompany")]
        public int TradeCompanyID { get; set; }
        [JsonIgnore]
        public virtual TradeCompany TradeCompany { get; set; }

    }
}
