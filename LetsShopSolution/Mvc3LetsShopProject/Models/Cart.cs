using System.ComponentModel.DataAnnotations;

namespace Mvc3LetsShopProject.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        //public int CustomerId { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
