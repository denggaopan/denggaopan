using Denggaopan.GraphqlDemo.Entities;
using GraphQL.Instrumentation;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Models
{
    public class InventoryMutation : ObjectGraphType
    {
        public InventoryMutation(IDataStore dataStore)
        {
            Field<ItemType>(
                "createItem",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<ItemInputType>>{Name = "item"}),
                resolve: context =>
                {
                    var item = context.GetArgument<Item>("item");
                    return dataStore.AddItemAsync(item);
                }
            );

            Field<OrderType, Order>()
                .Name("createOrder")
                .Argument<NonNullGraphType<OrderInputType>>("order", "order input")
                .ResolveAsync(ctx =>
                {
                    var order = ctx.GetArgument<Order>("order");
                    return dataStore.AddOrderAsync(order);
                });

            Field<CustomerType, Customer>()
                .Name("createCustomer")
                .Argument<NonNullGraphType<CustomerInputType>>("customer", "customer input")
                .ResolveAsync(ctx =>
                {
                    var customer = ctx.GetArgument<Customer>("customer");
                    return dataStore.AddCustomerAsync(customer);
                });

            Field<OrderItemType, OrderItem>()
                .Name("createOrderItem")
                .Argument<NonNullGraphType<OrderItemInputType>>("orderItem", "orderItem input")
                .ResolveAsync(ctx =>
                {
                    var orderItem = ctx.GetArgument<OrderItem>("orderItem");
                    return dataStore.AddOrderItemAsync(orderItem);
                });
        }
    }
}
