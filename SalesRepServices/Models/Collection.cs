using System;
using System.Collections.Generic;
using System.Text;

namespace SalesRepServices.Models
{
    public class Collection<T> : BaseModel
    {
        public T[] Value { get; set; }
    }
}
