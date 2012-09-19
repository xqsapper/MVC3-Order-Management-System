using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using DataClassLibrary;
using DataClassLibrary.Models;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Mvc3LetsShopProject.Controllers
{
    /// <summary>
    /// This controller is used to access and perform all the actions related to a product.
    /// </summary>

    public class ProductController : Controller
    {

        ICacheManager cache = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
        ICacheManager cache2 = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
        AbsoluteTime absolutetime = new AbsoluteTime(TimeSpan.FromDays(1));

        /// <summary>
        ///  GET: /Product/
        /// </summary>
        /// <returns></returns>

        public ActionResult Index(int? page)
        {
            List<int> orderidlist = new List<int>();
            
            List<AddToCartModel> cartlist = ShoppingCartImplementation.GetFromCart(@User.Identity.Name);

            CartAndAllProducts cp = new CartAndAllProducts();
            
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
                //productcacheData = (List<Product>)cache["ProductData"];
            }

            List<Product> prodlist = productcacheData;
            cp.productlist = prodlist;
            cp.cartlist = cartlist;

            return View(cp);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /Product?string=productname
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult Index(string ProductName)
        {
            try
            {
                if (!String.IsNullOrEmpty(ProductName))
                {
                    try
                    {
                        var product = LetsShopImplementation.SearchProduct(ProductName);

                        cache2.Add("SearchProductData", LetsShopImplementation.SearchProduct(ProductName), 
                                    CacheItemPriority.High, null, absolutetime);

                        List<int> orderidlist = new List<int>();
                       
                        CartAndAllProducts cp = new CartAndAllProducts();
                        List<AddToCartModel> cartlist = ShoppingCartImplementation.GetFromCart(@User.Identity.Name);
                        foreach (var x in cartlist)
                        {
                            orderidlist.Add(x.Product.ProductId);
                        }
                        ViewBag.Message = orderidlist;

                        if (product == null)
                        {
                            return RedirectToAction("Index", "Product");
                        }
                        else // if productname = "Alienware", search for "Alen", "Alien" and "Alienware" reaches here.
                        {
                            cp.productlist = product;
                            cp.cartlist = cartlist;

                            return View(cp);
                        }
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("ErrorPage", "Product");
                    }
                }
                else  // empty search reaches here.
                {
                    ViewBag.errormessage = " Enter a name to search the product!!";
                    List<int> orderidlist = new List<int>();

                    CartAndAllProducts cp = new CartAndAllProducts();
                    List<AddToCartModel> cartlist = ShoppingCartImplementation.GetFromCart(@User.Identity.Name);

                    var products = LetsShopImplementation.GetProducts();

                    foreach (var x in cartlist)
                    {
                        orderidlist.Add(x.Product.ProductId);
                    }
                    ViewBag.Message = orderidlist;

                    cp.productlist = products;
                    cp.cartlist = cartlist;

                    return View(cp);
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("ErrorPage", "Product");
            }

        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /Product/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
      
        public ActionResult Details(int id)
        {
            return View(LetsShopImplementation.GetProduct(id));
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /Product/CreateProduct
        /// </summary>
        /// <returns></returns>

        public ActionResult CreateProduct()
        {
            if (Session["usertype"] != null)
            {
                if (Session["usertype"].ToString() == "Admin")
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
                return RedirectToAction("Index", "Home");
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /Product/CreateProduct
        /// </summary>
        /// <param name="pcr"></param>
        /// <param name="file"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult CreateProduct(ProductCategoryRelation pcr, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("~/Images/" + fileName));
                    pcr.product.Picture = WebConfigurationManager.AppSettings["ImageUpload"] + fileName;
                }
                else
                {
                    pcr.product.Picture = WebConfigurationManager.AppSettings["ImageUpload"] + "defaultimage.jpg";
                }
                
                
                pcr.categories = LetsShopImplementation.GetCategoryIdByName(pcr.categories.CategoryName, pcr.categories.SubCategoryName);
                pcr.product.CategoryId = pcr.categories.CategoryId;
                pcr.product.SubCategoryId = pcr.categories.SubCategoryId;

                LetsShopImplementation.CreateProduct(pcr.product);

                cache.Add("ProductData", LetsShopImplementation.GetProducts(),
                                   CacheItemPriority.High, null, absolutetime);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return View();
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /Product/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Edit(int id)
        {
            if (Session["usertype"] != null)
            {
                if (Session["usertype"].ToString() == "Admin")
                {
                    return View(LetsShopImplementation.GetProduct(id));
                }
                else
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            else
                return RedirectToAction("Index", "Product");

        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /Product/Edit/5
        /// </summary>
        /// <param name="pcr"></param>
        /// <param name="id"></param>
        /// <param name="file"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Edit(ProductCategoryRelation pcr, int id, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    file.SaveAs(Server.MapPath("~/Images/" + fileName));
                    pcr.product.Picture = WebConfigurationManager.AppSettings["ImageUpload"] + fileName;
                }
                
                pcr.product.ProductId = id;

                pcr.categories = LetsShopImplementation.GetCategoryIdByName(pcr.categories.CategoryName, pcr.categories.SubCategoryName);
                pcr.product.CategoryId = pcr.categories.CategoryId;
                pcr.product.SubCategoryId = pcr.categories.SubCategoryId;

                LetsShopImplementation.UpdateProduct(pcr.product);

                cache.Add("ProductData", LetsShopImplementation.GetProducts(),
                                   CacheItemPriority.High, null, absolutetime);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return View();
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /Product/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        public ActionResult Delete(int id)
        {
            if (Session["usertype"] != null)
            {
                if (Session["usertype"].ToString() == "Admin")
                {
                    return View(LetsShopImplementation.GetProduct(id));
                }
                else
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            else
                return RedirectToAction("Index", "Product");
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /Product/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Delete(int id, Product product)
        {
            try
            {
                LetsShopImplementation.DeleteProduct(id);

                cache.Add("ProductData", LetsShopImplementation.GetProducts(),
                                   CacheItemPriority.High, null, absolutetime);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return View();
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/Add to Cart/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult AddToCart(int id)
        {
            try
            {
                ShoppingCartImplementation.AddToCart(id, @User.Identity.Name);
                return RedirectToAction("Index", "Product");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("Index", "Product");
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/Cart
        /// </summary>
        /// <returns></returns>

        public ActionResult Cart()
        {
            try
            {
                if (Session["usertype"] != null)
                {
                    return View(ShoppingCartImplementation.GetFromCart(@User.Identity.Name));
                }
                else
                    return RedirectToAction("Index", "Product");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("Index", "Product");
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/RemoveFromCart/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult RemoveFromCart(int id)
        {
            try
            {
                if (Session["usertype"] != null)
                {
                    string LoggedInUser = @User.Identity.Name;
                    ShoppingCartImplementation.RemoveFromCart(id, LoggedInUser);
                    return RedirectToAction("Cart", "Product" /*, new { id = LoggedInUser }*/);
                }
                else
                    return RedirectToAction("Index", "Product");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("Index", "Product");
            }
        }

        //______________________________________________________________________________________
        
        /// <summary>
        /// Product/RemoveFromFrontCart/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult RemoveFromFrontCart(int id)
        {
            try
            {
                if (Session["usertype"] != null)
                {
                    string LoggedInUser = @User.Identity.Name;
                    ShoppingCartImplementation.RemoveFromCart(id, LoggedInUser);
                    return RedirectToAction("Index", "Product" /*, new { id = LoggedInUser }*/);
                }
                else
                    return RedirectToAction("Index", "Product");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("Index", "Product");
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/Cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult IncrementQuantity(int id)
        {
            try
            {
                ShoppingCartImplementation.AddToCart(id, @User.Identity.Name);
                return RedirectToAction("Cart", "Product");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("Cart", "Product");
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/Cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult IncrementQuantityFront(int id)
        {
            try
            {
                ShoppingCartImplementation.AddToCart(id, @User.Identity.Name);
                return RedirectToAction("Index", "Product");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("Index", "Product");
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/Cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult DecrementQuantity(int id)
        {
            try
            {
                ShoppingCartImplementation.ReduceFromCart(id, @User.Identity.Name);
                return RedirectToAction("Cart", "Product");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("Cart", "Product");
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/Cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult DecrementQuantityFront(int id)
        {
            try
            {
                ShoppingCartImplementation.ReduceFromCart(id, @User.Identity.Name);
                return RedirectToAction("Index", "Product");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("Index", "Product");
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/EmptyCart/abd
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public ActionResult EmptyCart(string id)
        {
            try
            {
                if (Session["usertype"] != null)
                {
                    string LoggedInUser = @User.Identity.Name;
                    ShoppingCartImplementation.EmptyCart(id);

                    return RedirectToAction("Cart", "Product");
                }
                else
                    return RedirectToAction("Cart", "Product");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("Index", "Product");
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/CheckOut
        /// </summary>
        /// <returns></returns>

        public ActionResult CheckOut()
        {
            try
            {
                if (Session["usertype"] != null)
                {
                    string LoggedInUser = @User.Identity.Name;
                    return View(LetsShopImplementation.GetCheckOutDetails(LoggedInUser));
                }
                else
                    return RedirectToAction("Index", "Product");
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("Index", "Product");
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/CheckOut 
        /// </summary>
        /// <param name="checkout"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult CheckOut(CheckOutModel checkout)
        {
            try
            {
                checkout.userdata.UserId = @User.Identity.Name;

                ViewBag.checkOutMessage = LetsShopImplementation.PlaceOrder(checkout);
                ShoppingCartImplementation.EmptyCart(@User.Identity.Name);
                return View();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return View();
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Product/ErrorPage
        /// </summary>
        /// <returns></returns>
        
        public ViewResult ErrorPage()
        {
            return View();
        }

        //________________________________________________________________

        //Category browsing
        
        /// <summary>
        /// GET: /Product/Browse?Category=Books
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>

        public ActionResult Browse(string Category)
        {
            try
            {
                List<Product> products = LetsShopImplementation.GetProductsByCategoryName(Category);
                return View(products);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("ErrorPage", "Product");
            }
        }

        /// <summary>
        /// GET: /Product/Search?Category=Books&SubCategory=Fiction
        /// </summary>
        /// <param name="Category"></param>
        /// <param name="SubCategory"></param>
        /// <returns></returns>

        public ActionResult Search(string Category, string SubCategory)
        {
            try
            {
                List<Product> products = LetsShopImplementation.GetProductsBySubCategoryName(Category, SubCategory);
                return View(products);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("ErrorPage", "Product");
            }
        }

        //________________________________________________________________

        /// <summary>
        /// POST: /Product/Browse?Category=Books
        /// </summary>
        /// <param name="Category"></param>
        /// <param name="Max"></param>
        /// <param name="Min"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult Browse(string Category, int Max=0, int Min=0)
        {
            try
            {
                List<Product> products = LetsShopImplementation.PriceFilter(Category, Max, Min);
                return View(products);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                return View();
            }
        }

        //________________________________________________________________

    }

}
