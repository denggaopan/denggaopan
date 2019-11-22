using Denggaopan.GraphqlDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Models
{
    public interface IDataStore
    {
        IEnumerable<Item> GetItems();
        Item GetItemByBarcode(string barcode);
        Task<Item> AddItemAsync(Item model);

        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderAsync(int orderId);

        Task<IEnumerable<Customer>> GetCustomersAsync();

        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId);

        Task<Customer> GetCustomerByIdAsync(int customerId);
        Task<Order> AddOrderAsync(Order order);

        Task<Customer> AddCustomerAsync(Customer customer);


        Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);

        Task<OrderItem> GetOrderItemAsync(int id);

        Task<IEnumerable<OrderItem>> GetOrderItemByOrderIdAsync(int orderId);
    }
}
