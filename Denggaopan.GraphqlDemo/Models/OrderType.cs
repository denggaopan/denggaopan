using Denggaopan.GraphqlDemo.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.DataLoader;

namespace Denggaopan.GraphqlDemo.Models
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(IDataStore dataStore, IDataLoaderContextAccessor accessor)
        {
            Field(o => o.OrderId);
            Field(o => o.Tag);
            Field(o => o.CreatedAt);
            Field<CustomerType, Customer>()
                .Name("customer")
                .ResolveAsync(ctx => {
                    //return dataStore.GetCustomerByIdAsync(ctx.Source.CustomerId);
                    var customersLoader = accessor.Context.GetOrAddBatchLoader<int, Customer>("GetCustomersByIds", dataStore.GetCustomersByIdsAsync);
                    return customersLoader.LoadAsync(ctx.Source.CustomerId);
                });
            Field<ListGraphType<OrderItemType>, IEnumerable<OrderItem>>()
                .Name("orderItems")
                .Resolve(ctx => {
                    return dataStore.GetOrderItemsByOrderIdAsync(ctx.Source.OrderId).Result;
                });
        }
    }
}
