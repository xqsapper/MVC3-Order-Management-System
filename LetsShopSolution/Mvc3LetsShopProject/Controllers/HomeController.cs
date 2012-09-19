using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DataClassLibrary;
using DataClassLibrary.Models;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Mvc3LetsShopProject.Controllers
{
    /// <summary>
    /// The home controller is used mainly for displaying the home page.
    /// </summary>

    public class HomeController : Controller
    {
        ICacheManager cache = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
        ICacheManager cache2 = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
        AbsoluteTime absolutetime = new AbsoluteTime(TimeSpan.FromDays(1));

        /// <summary>
        /// GET: Home/Index
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Index()
        {
            List<int> orderidlist = new List<int>();
            List<AddToCartModel> cartlist = ShoppingCartImplementation.GetFromCart(@User.Identity.Name);

            foreach (var x in cartlist)
            {
                orderidlist.Add(x.Product.ProductId);
            }
            ViewBag.Message = orderidlist;

            List<Product> productcacheData = new List<Product>();
            productcacheData = (List<Product>)cache["ProductData"];

            if (productcacheData == null)
            {
                cache.Add("ProductData", LetsShopImplementation.GetProducts(),
                                                        CacheItemPriority.High, null, absolutetime);
                productcacheData = (List<Product>)cache["ProductData"];
            }

            return View(productcacheData);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: Home/About
        /// </summary>
        /// <returns></returns>
        
        public ActionResult About()
        {
            return View();
        }

        //______________________________________________________________________________________

    }
}
