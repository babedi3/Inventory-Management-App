using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using InventoryManagement.Data;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Services.Product;
using InventoryManagement.Services.Inventory;
using System.Linq;
using InventoryManagement.Data.Models;
using System;

namespace InventoryManagement.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly InventoryDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;


        public OrderService(
            InventoryDbContext db,
            ILogger<OrderService> logger,
            IProductService productService,
            IInventoryService inventoryService
            ) {
            _db = db;
            _logger = logger;
            _productService = productService;
            _inventoryService = inventoryService;
           }
        /// <summary>
        /// Gets all SalesOrders in the system
        /// </summary>
        /// <returns></returns>
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(so => so.Customer)
                .ThenInclude(customer => customer.PrimaryAddress)
                .Include(so => so.SalesOrderItems)
                .ThenInclude(item => item.Product)
                .ToList();
        }

        /// <summary>
        /// create open sales order
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            _logger.LogInformation("Generating new order");

            foreach(var item in order.SalesOrderItems)
            {
                item.Product = _productService.GetProductById(item.Product.Id);
                var inventoryId = _inventoryService.GetByProductId(item.Product.Id).Id;
                _inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = "Open Order Created",
                    Time = DateTime.UtcNow
                };
            }

            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }

        /// <summary>
        /// mark open sales order as paid
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<bool> MarkFulfilled(int id)
        {
            var now = DateTime.UtcNow;
            var order = _db.SalesOrders.Find(id);
            order.UpdatedOn = now;
            order.IsPaid = true;

            try
            {
                _db.SalesOrders.Update(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = $"Order {order.Id} closed: Invoice paid in full.",
                    Time = DateTime.UtcNow
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow
                };
            }
        }
    }
}
