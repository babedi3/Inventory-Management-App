using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly InventoryDbContext _db;

        public CustomerService(InventoryDbContext dbContext)
        {
            _db = dbContext;
        }
        public List<Data.Models.Customer> GetAllCustomers()
        {
           return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer =>  customer.LastName)
                .ToList();
        }
        /// <summary>
        /// Adds new customer record
        /// </summary>
        /// <param name="customer instance"></param>
        /// <returns>ServiceResponse<Customer></Customer></returns>
        public ServiceResponse<Data.Models.Customer> CreateCustomer(Data.Models.Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = true,
                    Message = "New customer added",
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }

            catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Customer>
                {
                    IsSuccess = false,
                    Message = e.StackTrace,
                    Time = DateTime.UtcNow,
                    Data = customer
                };
            }
        }
        /// <summary>
        /// Delete customer record
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ServiceResponse<bool></bool></returns>
       
        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);
            var now = DateTime.UtcNow;

            if (customer == null)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = false,
                    Message = "Customer to delete not found",
                    Data = false
                };
            }

            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();
            }

            catch (Exception e)
            {
                return new ServiceResponse<bool>
                {
                    Time = now,
                    IsSuccess = true,
                    Message = e.StackTrace,
                    Data = false
                };
            }
        }

        /// <summary>
        /// Retreives customer record by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Data.Models.Customer GetById(int id)
        {
            return _db.Customers.Find(id);
        }
    }
}
