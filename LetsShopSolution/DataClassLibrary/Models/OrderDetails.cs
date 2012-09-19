using System.ComponentModel.DataAnnotations;

namespace DataClassLibrary.Models
{
    public class OrderDetails2
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}