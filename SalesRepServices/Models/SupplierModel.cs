using SalesRepDAL.Attributes;
using SalesRepDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SalesRepServices.Models
{
    public class SupplierModel
    {
        [Key]
        public int SupplierID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Address { get; set; }
        [Required]
        [StringLength(22, MinimumLength = 10)]
        public string Phone { get; set; }
        [EmailValidation]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string AdditionalInfo { get; set; }
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
    }
}
