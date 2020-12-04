using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Data.Models
{
    public class CustomerAddress
    {
        public int id { get; set; }
        public DateTime CreatedOn { get; set;  }
        public DateTime UpdatedOn { get; set; }

        [MaxLength(100)]
        public string AddressLine1 { get; set; }

        [MaxLength(17)] //longest us city name is 17 characters
        public string City { get; set; }

        [MaxLength(10)]
        public string State { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        [MaxLength(56)]
        public string Country { get; set; }

    }
}
