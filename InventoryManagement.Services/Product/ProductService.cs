using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Data;
using InventoryManagement.Data.Models;

namespace InventoryManagement.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly InventoryDbContext _db;
        public ProductService(InventoryDbContext dbContext)
        {
            _db = dbContext;
        }

        /// Retrieves all products from db
        public List<Data.Models.Product> GetAllProducts()
        {
            return _db.Products.ToList();
        }

        /// Retrieves product from db by primary key
        public Data.Models.Product GetProductById(int id)
        {
            return _db.Products.Find(id);
        }
        
        ///Adds new product to db
        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
            try
            {
                _db.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10,
                };

                _db.ProductInventories.Add(newInventory);

                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product> {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSuccess = true,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product> {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false,
            };
        }
    }

        /// Archives product by setting bool IsArchived to true
        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try {
                var product = _db.Products.Find(id);
                product.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Data.Models.Product> {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Archived Product",
                    IsSuccess = true
                };
            }

            catch (Exception e) {
                return new ServiceResponse<Data.Models.Product> {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
        }
    }
}