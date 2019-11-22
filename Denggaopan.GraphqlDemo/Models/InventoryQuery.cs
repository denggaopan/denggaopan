using Denggaopan.GraphqlDemo.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Models
{
    public class InventoryQuery : ObjectGraphType
    {
        public InventoryQuery(IDataStore dataStore)
        {
            Field<ItemType>("item",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "barcode" }),
                resolve: context =>
                {
                    var barcode = context.GetArgument<string>("barcode");
                    return dataStore.GetItemByBarcode(barcode);
                }
            );

            Field<ListGraphType<OrderType>, IEnumerable<Order>>()
                .Name("orders")
                .ResolveAsync(ctx =>
                {
                    return dataStore.GetOrdersAsync();
                });

            Field<ListGraphType<CustomerType>, IEnumerable<Customer>>()
                .Name("customers")
                .ResolveAsync(ctx =>
                {
                    return dataStore.GetCustomersAsync();
                });

            Field<OrderType>("order",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "orderId" }),
                resolve: context =>
                {
                    var orderId = context.GetArgument<int>("orderId");
                    return dataStore.GetOrderAsync(orderId);
                }
            );

            Field<CustomerType>("customer",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "customerId" }),
                resolve: context =>
                {
                    var customerId = context.GetArgument<int>("customerId");
                    return dataStore.GetCustomerByIdAsync(customerId);
                }
            );

            Field<OrderItemType>("orderItem",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return dataStore.GetOrderItemAsync(id);
                }
            );
        }
    }
}
