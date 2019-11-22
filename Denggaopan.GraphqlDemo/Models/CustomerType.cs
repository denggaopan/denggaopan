using Denggaopan.GraphqlDemo.Entities;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Models
{
    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType(IDataStore dataStore)
        {
            Field(c => c.CustomerId);
            Field(c => c.Name);
            Field(c => c.BillingAddress);
            Field<ListGraphType<OrderType>, IEnumerable<Order>>()
                .Name("orders")
                .Resolve(ctx => {
                    return dataStore.GetOrdersByCustomerIdAsync(ctx.Source.CustomerId).Result;
                });
        }
    }
}
