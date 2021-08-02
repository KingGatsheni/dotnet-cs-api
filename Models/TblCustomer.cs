using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace dotnet_cs_api.Models
{
    public class TblCustomer
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Residence { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}