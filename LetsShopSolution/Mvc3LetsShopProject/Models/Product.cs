using System.ComponentModel.DataAnnotations;
using System;

namespace Mvc3LetsShopProject.Models
{
    [Serializable]
    public class Product
    {
        [Key]
        public int ProductId { set; get; }
        public string ProductName { set; get; }
        public string ProductDescription { set; get; }
        public int SubCategoryId { set; get; }
        public double Price { set; get; }
        public int UnitsInStock { set; get; }
        public int StockAvailability { get; set; }
        public string Colour { set; get; }
        public string Size { set; get; }
        public string Picture { set; get; }
    }

}
