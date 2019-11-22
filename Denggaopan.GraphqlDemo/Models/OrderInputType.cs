using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Models
{
    public class OrderInputType : InputObjectGraphType
    {
        public OrderInputType()
        {
            Name = "OrderInput";
            Field<NonNullGraphType<StringGraphType>>("tag");
            Field<NonNullGraphType<DateGraphType>>("createdAt");
            Field<NonNullGraphType<IntGraphType>>("customerId");
        }
    }
}
