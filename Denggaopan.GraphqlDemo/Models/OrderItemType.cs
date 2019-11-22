using Denggaopan.GraphqlDemo.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Models
{
    public class OrderItemType : ObjectGraphType<OrderItem>
    {
        public OrderItemType(IDataStore dataStore)
        {
            Field(o => o.Id);
            Field(o => o.Quantity);
            Field(o => o.Barcode);
            Field(o => o.OrderId);
            Field<ItemType, Item>()
                .Name("item")
                .Resolve(ctx => { return dataStore.GetItemByBarcode(ctx.Source.Barcode); });
            Field<OrderType, Order>()
                .Name("order")
                .Resolve(ctx => { return dataStore.GetOrderAsync(ctx.Source.OrderId).Result; });
        }
    }
}
