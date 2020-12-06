using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Data.Models;
using InventoryManagement.Web.ViewModels;

namespace InventoryManagement.Web.Serialization
{
    /// <summary>
    /// Handles mapping order data models to and from related view models
    /// </summary>
    public static class OrderMapper
    {
        /// <summary>
        /// Maps InvoiceModel view model to SalesOrder data model
        /// </summary>
        public static SalesOrder SerializeInvoiceToOrder(InvoiceModel invoice)
        {
            var salesOrderItems = invoice.LineItems
                .Select(item => new SalesOrderItem
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerializeProductModel(item.Product)
                }).ToList();

            return new SalesOrder
                {
                    SalesOrderItems = salesOrderItems,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow
                };
        }
        /// <summary>
        /// Maps collection of SalesOrders (data) to DataModels (viewwmodels)
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static List<OrderModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderModel
            {
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                SalesOrderItems = SerializeSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList();
        }
        /// <summary>
        /// Maps collection of SalesOrderItems (data) to SalesOrderItemModels (viewmodels)
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        private static List<SalesOrderItemModel> SerializeSalesOrderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)
            }).ToList();
        }
    }
}
