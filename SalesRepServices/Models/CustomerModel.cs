using System.ComponentModel.DataAnnotations;

namespace SalesRepServices.Models
{
    public class CustomerModel
    {
        public int CusomerID { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Phone { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
