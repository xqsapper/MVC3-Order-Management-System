using System.Collections.Generic;
using DataClassLibrary.Models;

namespace DataClassLibrary
{
    public class LetsShopImplementation
    {
        /// <summary>
        /// This method is used to register a new user in the database.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Password"></param>
        /// <param name="EmailId"></param>
        /// <returns></returns>
        
        public static string CreateUser(string UserId, string Password, string EmailId)
        {
            UserSignUp us = new UserSignUp();
            us.UserId = UserId;
            us.Password = Password;
            us.EmailId = EmailId;
            return DataProvider.Registration(us);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to login a registered user and grant corresponding privileges.
        /// </summary>
        /// <param name="AccessName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        
        public static string ValidateUser(string AccessName, string Password)
        {
            return DataProvider.Login(AccessName, Password);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method creates a new product in the database. It can be accessed only by an administrator.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        
        public static string CreateProduct(Product product)
        {
            return DataProvider.AddProductToDB(product);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to search for an existing product in the database by its name. (Can be accessed by all users.)
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        
        public static List<Product> SearchProduct(string ProductName)
        {
            return DataProvider.ProductSearch(ProductName);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to retrieve all the products from the database for display.
        /// </summary>
        /// <returns></returns>
        
        public static List<Product> GetProducts()
        {
            return DataProvider.GetProducts();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// An administrator uses this method to keep a track on all the users registered with the website.
        /// </summary>
        /// <returns></returns>
        
        public static List<UserData> GetAllUsers()
        {
            return DataProvider.GetAllUsers();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// An administrator can use this method to get all the orders registered in the website.
        /// </summary>
        /// <returns></returns>
        
        public static List<Orders> GetAllOrders()
        {
            return DataProvider.GetAllOrders();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to get the information regarding a particular (logged in) user.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        
        public static UserData GetUserDetailsById(int CustomerId)
        {
            return DataProvider.GetUserDetailsById(CustomerId);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method gets the details of the logged user in order to display it in his account summary page.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        
        public static UserData GetUserDetailsByUserName(string UserId)
        {
            return DataProvider.GetUserDetailsByUserName(UserId);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method gets all the products in a particular category.
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        
        public static List<Product> GetProductsByCategoryName(string CategoryName)
        {
            return DataProvider.GetProductsByCategoryName(CategoryName);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method gets all the products in a particular subcategory.
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <param name="SubCategoryName"></param>
        /// <returns></returns>
        
        public static List<Product> GetProductsBySubCategoryName(string CategoryName, string SubCategoryName)
        {
            return DataProvider.GetProductsBySubCategoryName(CategoryName, SubCategoryName);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method returns a list of all the different categories in the database.
        /// </summary>
        /// <returns></returns>
        
        public static List<string> GetAllCategories()
        {
            return DataProvider.GetAllCategories();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used by a registered user to update his information (such as default shipping address, card no. etc).
        /// </summary>
        /// <param name="ud"></param>
        /// <returns></returns>
        
        public static string UpdateUser(UserData ud)
        {
            return DataProvider.UpdateUser(ud);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Gets the details of the particular product through its ProductId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public static ProductCategoryRelation GetProduct(int id)
        {
            return DataProvider.GetProduct(id);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used by an administrator to delete an existing product from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public static string DeleteProduct(int id)
        {
            return DataProvider.Delete(id);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// An administrator uses this method to update the details of an existing product in the database.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        
        public static string UpdateProduct(Product product)
        {
            return DataProvider.UpdateProduct(product);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to populate the user and the cart details during the checkout of an order.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        
        public static CheckOutModel GetCheckOutDetails(string UserId)
        {
            CheckOutModel CheckOut = new CheckOutModel();
            UserData userdata = DataProvider.GetUserDetailsByUserName(UserId);
            CheckOut.userdata = userdata;
            CheckOut.CartList = ShoppingCartImplementation.GetFromCart(UserId);
            double total = 0;
            foreach (var item in CheckOut.CartList)
            {
                total = total + item.Cart.Quantity * item.Product.Price;
                CheckOut.Total = total;
            }
            return CheckOut;
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to fetch all the information from the cart and the user, during the placing of an order, and saves the order in the database.
        /// </summary>
        /// <param name="userupdate"></param>
        /// <returns></returns>

        public static List<string> PlaceOrder(CheckOutModel userupdate)
        {
            return DataProvider.PlaceOrder(userupdate);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// Any registered user uses this method to cancel his order after placing, provided it is not yet shipped.
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        
        public static string CancelMyOrder(int orderid, string userid)
        {
            return DataProvider.CancelMyOrder(orderid, userid);
        }

        //______________________________________________________________________________________


        /// <summary>
        /// A registered user can use this method to get the history of all his orders.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        
        public static List<Orders> GetMyOrders(string userid)
        {
            return DataProvider.GetMyOrders(userid);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to get full information of a particular order, and helps an user track his order.
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        
        public static OrderInformation GetOrderDetails(int orderid)
        {
            return DataProvider.GetOrderDetails(orderid);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method filters the price of the items between a specified range.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>

        public static List<Product> PriceFilter(string categoryname, int max, int min)
        {
            return DataProvider.PriceFilter(categoryname, max, min);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method returns the corresponding category id from the category name.
        /// </summary>
        /// <param name="catname"></param>
        /// <param name="subcatname"></param>
        /// <returns></returns>
        
        public static Categories GetCategoryIdByName(string catname, string subcatname)
        {
            return DataProvider.GetCategoryIdByName(catname, subcatname);
        }
        
        //______________________________________________________________________________________

        /// <summary>
        /// This method is used by an administrator to change the order status from 'Processing' to 'Shipped'.
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="UserId"></param>
        /// <param name="TransactionStatus"></param>
        /// <returns></returns>
        
        public static string OrderStatus(int orderid, string UserId, string TransactionStatus)
        {
            return DataProvider.OrderStatus(orderid, UserId, TransactionStatus);
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method gets the details of the logged user in order to display it in his account summary page.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public static UserData ForgotPassword(string id)
        {
            UserData userdata = DataProvider.GetUserDetailsByUserName2(id);
            return userdata;
        }

        /// <summary>
        /// This method is used during login by a security answer, when a user forgets his/her password.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SecurityAnswer"></param>
        /// <returns></returns>

        public static UserCredentials LoginAfterForgotPassword(string UserId, string SecurityAnswer)
        {
            return DataProvider.LoginAfterForgotPassword(UserId, SecurityAnswer);
        }


    }

}
