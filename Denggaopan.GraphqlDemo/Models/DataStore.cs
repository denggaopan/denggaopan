using Denggaopan.GraphqlDemo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Models
{
    public class DataStore :IDataStore
    {
        private readonly ApplicationDbContext _db;
        public DataStore(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Item> GetItems()
        {
            return _db.Items;
        }

        public Item GetItemByBarcode(string barcode)
        {
            return _db.Items.FirstOrDefault(i => i.Barcode.Equals(barcode));
        }

        public async Task<Item> AddItemAsync(Item model)
        {
            var addedItem = await _db.Items.AddAsync(model);
            await _db.SaveChangesAsync();
            return addedItem.Entity;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _db.Orders.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _db.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _db.Orders.Where(a => a.CustomerId == customerId).AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _db.Customers.AsNoTracking().FirstOrDefaultAsync(a=>a.CustomerId == customerId);
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            var addedModel = await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return addedModel.Entity;
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            var addedModel = await _db.Customers.AddAsync(customer);
            await _db.SaveChangesAsync();
            return addedModel.Entity;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            return await _db.Orders.FirstOrDefaultAsync(a=>a.OrderId == orderId);
        }





        public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
        {
            var addedModel = await _db.OrderItems.AddAsync(orderItem);
            await _db.SaveChangesAsync();
            return addedModel.Entity;
        }

        public async Task<OrderItem> GetOrderItemAsync(int id)
        {
            return await _db.OrderItems.FindAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _db.OrderItems.Where(a => a.OrderId == orderId).AsNoTracking().ToListAsync();
        }

        public async Task<IDictionary<int, Customer>> GetCustomersByIdsAsync(IEnumerable<int> customerIds, CancellationToken token)
        {
            return await _db.Customers.Where(i => customerIds.Contains(i.CustomerId)).ToDictionaryAsync(x => x.CustomerId);
        }
    }
}
