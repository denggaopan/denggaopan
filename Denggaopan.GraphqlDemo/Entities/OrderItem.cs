using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Entities
{
    [Table("OrderItem")]
    public class OrderItem
    {
        public int Id { get; set; }

        public string Barcode { get; set; }

        [ForeignKey("Barcode")]
        public virtual Item Item { get; set; }

        public int Quantity { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
