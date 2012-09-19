using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc3LetsShopProject.Models
{
    public class AddToCartModel
    {
        public Product Product { set; get; }
        //public CustomerDetails CustomerDetails { set; get; }
        public Cart Cart { set; get; }
        public UserData UserData { get; set; }
    }
    public class CheckOutModel
    {
        public List<AddToCartModel> Cart { set; get; }
        public UserData userdata { get; set; }
        public Double Total { get; set; }
    }

}