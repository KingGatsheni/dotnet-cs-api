using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace dotnet_cs_api.Models
{
    public class TblProduct
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PackSize { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string Discription { get; set; }
        public string ProductImage { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}