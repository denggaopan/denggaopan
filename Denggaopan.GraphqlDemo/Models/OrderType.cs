using Denggaopan.GraphqlDemo.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Models
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(IDataStore dataStore)
        {
            Field(o => o.OrderId);
            Field(o => o.Tag);
            Field(o => o.CreatedAt);
            Field<CustomerType, Customer>()
                .Name("customer")
                .Resolve(ctx => {
                    return dataStore.GetCustomerByIdAsync(ctx.Source.CustomerId).Result;
                });
        }
    }
}
