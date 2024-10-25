using System;
using System.Collections.Generic;

namespace CoffeApp.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? TableNumber { get; set; }
        public string? Phone { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
