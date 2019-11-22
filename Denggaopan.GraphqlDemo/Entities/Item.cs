using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Entities
{
    [Table("Item")]
    public class Item
    {
        [Key]
        public string Barcode { get; set; }

        public string Title { get; set; }

        public decimal SellingPrice { get; set; }


        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
