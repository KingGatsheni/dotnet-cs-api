using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_cs_api.Models
{
    public class TblOrderItem
    {
        [Key]
        public int OrderItemId { get; set; }    
        public int OrderId { get; set; }
        public TblOrder Order { get; set; }
        public int ProductId { get; set; }
        public TblProduct Product { get; set; }
        public int Quantity { get; set; }

       
        
        
    }
}