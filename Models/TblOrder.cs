using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_cs_api.Models
{
    public class TblOrder
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public TblCustomer Customer { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public bool OrderStatus { get; set; }
        public List<TblOrderItem> OrderItems { get; set; }

    }
}