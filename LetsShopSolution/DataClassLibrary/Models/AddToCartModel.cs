using System;
using System.Collections.Generic;

namespace DataClassLibrary.Models
{
    public class AddToCartModel
    {
        public Product Product { set; get; }
        public Cart Cart { set; get; }
    }
    //______________________________________________________________________________________

    public class CartAndAllProducts
    {
        public List<Product> productlist { get; set; }
        public List<AddToCartModel> cartlist { get; set; }
    }

    //______________________________________________________________________________________
    
    public class CheckOutModel
    {
        public List<AddToCartModel> CartList { set; get; }
        public UserData userdata { get; set; }
        public Double Total { get; set; }
    }

    //______________________________________________________________________________________
    
    public class OrderInformation
    {
        public List<OrderDetails> orderproduct { set; get; }
        public Orders orderuser { get; set; }
    }

    //______________________________________________________________________________________
    
    public class OrderDetails
    {
        public Product product { get; set; }
        public int quantity { get; set; }
    }

    //______________________________________________________________________________________

    public class ProductCategoryRelation
    {
        public Product product { get; set; }
        public Categories categories { get; set; }
    }

    //______________________________________________________________________________________


}