using System;
using System.Collections.Generic;

namespace InventoryManagement.Web.ViewModels
{
    /// <summary>
    /// view mdoel for open salesorders
    /// </summary>
    public class InvoiceModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CustomerId { get; set; }
        public List<SalesOrderItemModel> LineItems { get; set; }
    }
     /// <summary>
     /// view model for salesorderitems
     /// </summary>
    public class SalesOrderItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductModel Product { get; set; }
    }
}
