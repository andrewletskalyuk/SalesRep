using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SalesRepServices.Models
{
    public class CustomerModel
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Phone { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
