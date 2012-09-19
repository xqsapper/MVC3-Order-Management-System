using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataClassLibrary.Models
{
    [Serializable]
    public class Product
    {
        [Key]
        public int ProductId { set; get; }

        [Required(ErrorMessage = "ProductName is required")]
        public string ProductName { set; get; }

        [Required(ErrorMessage = "ProductDescription is required")]
        public string ProductDescription { set; get; }

        [Required(ErrorMessage = "CategoryId is required")]
        [RegularExpression("[0-9]{1}", ErrorMessage = "CategoryId is a 1-digit number.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "SubCategoryId is required")]
        [RegularExpression("[0-9]{1,2}", ErrorMessage = "SubCategoryId is a 1 or 2-digit number.")]
        public int SubCategoryId { set; get; }

        [Required(ErrorMessage = "Price is required")]
        [RegularExpression("[0-9]{2,12}", ErrorMessage = "Price contains only digits and should be between 2 and 12 digits.")]
        public double Price { set; get; }

        [Required(ErrorMessage = "UnitsInStock is required")]
        [RegularExpression("[0-9]{2,10}", ErrorMessage = "Units in Stock is a number between and 1 and 10 digits.")]
        public int UnitsInStock { set; get; }

        [Required(ErrorMessage = "StockAvailability is required")]
        [RegularExpression("[0-9]{1}", ErrorMessage = "Stock Availability must be a 1-digit number.")]
        public int StockAvailability { get; set; }

        [Required(ErrorMessage = "Colour is required")]
        public string Colour { set; get; }

        [Required(ErrorMessage = "Size is required")]
        [RegularExpression("[0-9]{1,4}", ErrorMessage = "Size can be of at max 4 digits.")]
        public string Size { set; get; }

        public string Picture { set; get; }
    }


    //______________________________________________________________________________________

    public interface IProductRepository
    {
        IList<Product> FindAll();
        Product FindByName(string productName);
        Product FindById(int productId);
        bool Save(Product target);
    }

    //______________________________________________________________________________________

    public class ProductRepository : IProductRepository
    {
        #region IProductRepository Members

        public IList<Product> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public Product FindByName(string productName)
        {
            throw new System.NotImplementedException();
        }

        public Product FindById(int productId)
        {
            throw new System.NotImplementedException();
        }

        public bool Save(Product target)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }

    //______________________________________________________________________________________

}
