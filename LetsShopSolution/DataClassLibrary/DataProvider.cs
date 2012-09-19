using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DataClassLibrary.Models;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Transactions;

namespace DataClassLibrary
{
    /// <summary>
    /// This class file is used for accessing the database and performing all the operations on it.
    /// </summary>
    class DataProvider
    {
        /// <summary>
        /// This method is used to register a new user in the database.
        /// </summary>
        /// <param name="usersignup"></param>
        /// <returns></returns>
        
        public static string Registration(UserSignUp usersignup)
        {
            try
            {
                Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
                DbCommand CmdObj = db.GetStoredProcCommand("SignUp");
                db.AddInParameter(CmdObj, "@UserId", DbType.String, usersignup.UserId);
                db.AddInParameter(CmdObj, "@Password", DbType.String, usersignup.Password);
                db.AddInParameter(CmdObj, "@EmailId", DbType.String, usersignup.EmailId);
                db.AddOutParameter(CmdObj, "@strMessage", DbType.String, 255);
                db.ExecuteNonQuery(CmdObj);
                return db.GetParameterValue(CmdObj, "@strMessage").ToString();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Policy");
                if (rethrow) throw;
                string result = " Error in Registration.";
                return result;
            }

        }
        //_____________________________________________________________________
        
        /// <summary>
        /// This method is used to login a registered user and grant corresponding privileges.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        
        public static string Login(string user, string pwd)
        {
            try
            {
                Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
                DbCommand CmdObj = db.GetStoredProcCommand("Login");
                db.AddInParameter(CmdObj, "@UserId", DbType.String, user);
                db.AddInParameter(CmdObj, "@Password", DbType.String, pwd);
                db.AddOutParameter(CmdObj, "@strMessage", DbType.String, 255);
                db.ExecuteNonQuery(CmdObj);
                return db.GetParameterValue(CmdObj, "@strMessage").ToString();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Policy");
                if (rethrow) throw;
                string result = " Error in Login.";
                return result;
            }
        }
        //_____________________________________________________________________
        
        /// <summary>
        /// This method creates a new product in the database. It can be accessed only by an administrator.
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        
        public static string AddProductToDB(Product prod)
        {
            try
            {
                Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
                DbCommand CmdObj = db.GetStoredProcCommand("CreateProduct");
                db.AddInParameter(CmdObj, "@ProductName", DbType.String, prod.ProductName);
                db.AddInParameter(CmdObj, "@ProductDescription", DbType.String, prod.ProductDescription);
                db.AddInParameter(CmdObj, "@CategoryId", DbType.Int32, prod.CategoryId);
                db.AddInParameter(CmdObj, "@SubCategoryId", DbType.Int32, prod.SubCategoryId);
                db.AddInParameter(CmdObj, "@Price", DbType.Decimal, prod.Price);
                db.AddInParameter(CmdObj, "@UnitsInStock", DbType.Double, prod.UnitsInStock);
                db.AddInParameter(CmdObj, "@StockAvailability", DbType.Int32, prod.StockAvailability);
                db.AddInParameter(CmdObj, "@Colour", DbType.String, prod.Colour);
                db.AddInParameter(CmdObj, "@Size", DbType.String, prod.Size);
                db.AddInParameter(CmdObj, "@Picture", DbType.String, prod.Picture);
                db.AddOutParameter(CmdObj, "@strMessage", DbType.String, 255);
                db.ExecuteNonQuery(CmdObj);
                return db.GetParameterValue(CmdObj, "@strMessage").ToString();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Policy");
                if (rethrow) throw;
                string result = " Error in adding product to database.";
                return result;
            }
        }
        //_____________________________________________________________________
        
        /// <summary>
        /// An administrator uses this method to update the details of an existing product in the database.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        
        public static string UpdateProduct(Product product)
        {
            try
            {
                string result;
                Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
                DbCommand cmdObj = _db.GetStoredProcCommand("UpdateProduct");
                _db.AddInParameter(cmdObj, "@ProductId", DbType.Int32, Convert.ToInt32(product.ProductId));
                _db.AddInParameter(cmdObj, "@ProductName", DbType.String, product.ProductName);
                _db.AddInParameter(cmdObj, "@ProductDescription", DbType.String, product.ProductDescription);
                _db.AddInParameter(cmdObj, "@CategoryId", DbType.Int32, product.CategoryId);
                _db.AddInParameter(cmdObj, "@SubCategoryId", DbType.Int32, product.SubCategoryId);
                _db.AddInParameter(cmdObj, "@Price", DbType.Double, product.Price);
                _db.AddInParameter(cmdObj, "@Colour", DbType.String, product.Colour);
                _db.AddInParameter(cmdObj, "@Size", DbType.String, product.Size);
                _db.AddInParameter(cmdObj, "@UnitsInStock", DbType.Double, product.UnitsInStock);
                _db.AddInParameter(cmdObj, "@StockAvailability", DbType.Double, product.StockAvailability);
                _db.AddInParameter(cmdObj, "@Picture", DbType.String, product.Picture);
                _db.AddOutParameter(cmdObj, "@strMessage", DbType.String, 255);
                _db.ExecuteNonQuery(cmdObj);
                result = _db.GetParameterValue(cmdObj, "@strMessage").ToString();
                return result;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Policy");
                if (rethrow) throw;
                string result = " Error in Updating Product.";
                return result;
            }

        }
        //_____________________________________________________________________

        /// <summary>
        /// This method is used by an administrator to delete an existing product from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public static string Delete(int id)
        {
            string result;
            try
            {
                Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
                DbCommand cmdObj = _db.GetStoredProcCommand("DeleteProduct");
                _db.AddInParameter(cmdObj, "@ProductId", DbType.Int32, Convert.ToInt32(id));
                _db.AddOutParameter(cmdObj, "@Message", DbType.String, 255);
                _db.ExecuteNonQuery(cmdObj);
                result = _db.GetParameterValue(cmdObj, "@Message").ToString();
                return result;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Policy");
                if (rethrow) throw;
                result = " Error in deleting product.";
                return result;
            }
        }
        //_____________________________________________________________________
        
        /// <summary>
        /// This method is used to search for an existing product in the database by its name. (Can be accessed by all users.)
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        
        public static List<Product> ProductSearch(string ProductName)
        {
            var prodlist = new List<Product>();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("SearchProduct");
            _db.AddInParameter(cmdObj, "@ProductName", DbType.String, ProductName);
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    var prod = new Product();
                    prod.ProductId = Convert.ToInt32(dataReader["ProductId"].ToString());
                    prod.ProductName = dataReader["ProductName"].ToString();
                    prod.ProductDescription = dataReader["ProductDescription"].ToString();
                    prod.Price = Convert.ToDouble(dataReader["Price"]);
                    prod.UnitsInStock = Convert.ToInt32(dataReader["UnitsInStock"]);
                    prod.StockAvailability = Convert.ToInt32(dataReader["StockAvailability"]);
                    prod.Colour = dataReader["Colour"].ToString();
                    prod.Size = dataReader["Size"].ToString();
                    prod.Picture = dataReader["Picture"].ToString();
                    prodlist.Add(prod);
                }
                return prodlist;
            }
        }
        //_____________________________________________________________________
        
        /// <summary>
        /// This method is used to retrieve all the products from the database for display. 
        /// </summary>
        /// <returns></returns>
        
        public static List<Product> GetProducts()
        {
            var prodlist = new List<Product>();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetProducts");
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    var prod = new Product();
                    prod.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    prod.ProductName = dataReader["ProductName"].ToString();
                    prod.ProductDescription = dataReader["ProductDescription"].ToString();
                    prod.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    prod.SubCategoryId = Convert.ToInt32(dataReader["SubCategoryId"]);
                    prod.Price = Convert.ToDouble(dataReader["Price"]);
                    prod.UnitsInStock = Convert.ToInt32(dataReader["UnitsInStock"]);
                    prod.StockAvailability = Convert.ToInt32(dataReader["StockAvailability"]); 
                    prod.Colour = dataReader["Colour"].ToString();
                    prod.Size = dataReader["Size"].ToString();
                    prod.Picture = dataReader["Picture"].ToString();
                    prodlist.Add(prod);
                }
                return prodlist;
            }
        }
        //_____________________________________________________________________
        
        /// <summary>
        /// An administrator uses this method to keep a track on all the users registered with the website.
        /// </summary>
        /// <returns></returns>
        
        public static List<UserData> GetAllUsers()
        {
            var userslist = new List<UserData>();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetUsers");
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    var user = new UserData();
                    user.CustomerId = dataReader["CustomerId"].ToString();
                    user.UserId = dataReader["UserId"].ToString();
                    user.EmailId = dataReader["EmailId"].ToString();
                    user.FirstName = dataReader["FirstName"].ToString();
                    user.LastName = dataReader["LastName"].ToString();
                    user.BillingAddress = dataReader["BillingAddress"].ToString();
                    user.ShippingAddress = dataReader["ShippingAddress"].ToString();
                    
                    long cardNumber;
                    long.TryParse(dataReader["CardNumber"].ToString(),out cardNumber);
                    user.CardNumber = cardNumber;

                    user.CardType = dataReader["CardType"].ToString();

                    DateTime cardExpiry;
                    DateTime.TryParse(dataReader["CardExpiryDate"].ToString(), out cardExpiry);
                    user.CardExpiryDate = cardExpiry;

                    long phoneNo;
                    long.TryParse(dataReader["PhoneNumber"].ToString(), out phoneNo);
                    user.PhoneNumber = phoneNo;

                    userslist.Add(user);
                }
                return userslist;
            }

        }
        //_____________________________________________________________________
        
        /// <summary>
        /// This method is used to get the information regarding a particular (logged in) user.
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        
        public static UserData GetUserDetailsById(int CustomerId)
        {
            var user = new UserData();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetUserById");
            _db.AddInParameter(cmdObj, "@CustomerId", DbType.Int32, CustomerId);
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    user.CustomerId = dataReader["CustomerId"].ToString();
                    user.UserId = dataReader["UserId"].ToString();
                    user.EmailId = dataReader["EmailId"].ToString();
                    user.Password = "••••••••";
                    user.FirstName = dataReader["FirstName"].ToString();
                    user.LastName = dataReader["LastName"].ToString();
                    user.BillingAddress = dataReader["BillingAddress"].ToString();
                    user.ShippingAddress = dataReader["ShippingAddress"].ToString();

                    long cardNumber;
                    long.TryParse(dataReader["CardNumber"].ToString(), out cardNumber);
                    user.CardNumber = cardNumber;

                    user.CardType = dataReader["CardType"].ToString();

                    DateTime cardExpiry;
                    DateTime.TryParse(dataReader["CardExpiryDate"].ToString(), out cardExpiry);
                    
                    user.CardExpiryDate = cardExpiry;

                    long phoneNo;
                    long.TryParse(dataReader["PhoneNumber"].ToString(), out phoneNo);
                    user.PhoneNumber = phoneNo;
                    
                }
                return user;
            }
        }
        //_____________________________________________________________________
        
        /// <summary>
        /// This method gets the details of the logged user in order to display it in his account summary page.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        
        public static UserData GetUserDetailsByUserName(string UserId)
        {
            var user = new UserData();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetUserByUsername");
            _db.AddInParameter(cmdObj, "@UserId", DbType.String, UserId);
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    user.CustomerId = dataReader["CustomerId"].ToString();
                    user.UserId = dataReader["UserId"].ToString();
                    user.EmailId = dataReader["EmailId"].ToString();
                    user.Password = dataReader["Password"].ToString();
                    user.SecurityQuestion = dataReader["SecurityQuestion"].ToString();
                    user.SecurityAnswer = dataReader["SecurityAnswer"].ToString();
                    user.FirstName = dataReader["FirstName"].ToString();
                    user.LastName = dataReader["LastName"].ToString();
                    user.BillingAddress = dataReader["BillingAddress"].ToString();
                    user.ShippingAddress = dataReader["ShippingAddress"].ToString();

                    long cardNumber;
                    long.TryParse(dataReader["CardNumber"].ToString(), out cardNumber);
                    user.CardNumber = cardNumber;

                    user.CardType = dataReader["CardType"].ToString();

                    DateTime cardExpiry;
                    DateTime.TryParse(dataReader["CardExpiryDate"].ToString(), out cardExpiry);
                    user.CardExpiryDate = cardExpiry;

                    long phoneNo;
                    long.TryParse(dataReader["PhoneNumber"].ToString(), out phoneNo);
                    user.PhoneNumber = phoneNo;

                }
                return user;
            }
        }
        
        //_____________________________________________________________________

        /// <summary>
        /// This method gets the details of the logged user in order to display it in his account summary page.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>

        public static UserData GetUserDetailsByUserName2(string UserId)
        {
            var user = new UserData();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetUserByUsername2");
            _db.AddInParameter(cmdObj, "@UserId", DbType.String, UserId);
            _db.AddOutParameter(cmdObj, "@strMessage", DbType.String, 255);

            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    user.CustomerId = dataReader["CustomerId"].ToString();
                    user.UserId = dataReader["UserId"].ToString();
                    user.EmailId = dataReader["EmailId"].ToString();
                    user.Password = dataReader["Password"].ToString();
                    user.SecurityQuestion = dataReader["SecurityQuestion"].ToString();
                    user.FirstName = dataReader["FirstName"].ToString();
                    user.LastName = dataReader["LastName"].ToString();
                    user.BillingAddress = dataReader["BillingAddress"].ToString();
                    user.ShippingAddress = dataReader["ShippingAddress"].ToString();

                    long cardNumber;
                    long.TryParse(dataReader["CardNumber"].ToString(), out cardNumber);
                    user.CardNumber = cardNumber;

                    user.CardType = dataReader["CardType"].ToString();

                    DateTime cardExpiry;
                    DateTime.TryParse(dataReader["CardExpiryDate"].ToString(), out cardExpiry);
                    user.CardExpiryDate = cardExpiry;

                    long phoneNo;
                    long.TryParse(dataReader["PhoneNumber"].ToString(), out phoneNo);
                    user.PhoneNumber = phoneNo;

                }
                return user;
            }
        }

        //_____________________________________________________________________

        /// <summary>
        /// Gets the details of the particular product through its ProductId.
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        
        public static ProductCategoryRelation GetProduct(int productid)
        {
            ProductCategoryRelation pc = new ProductCategoryRelation();
            Product prod = new Product();
            Categories cat = new Categories();

            prod.ProductId = productid;
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetProductById");
            _db.AddInParameter(cmdObj, "@ProductId", DbType.Int32, Convert.ToInt32(productid));

            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    prod.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    prod.ProductName = dataReader["ProductName"].ToString();
                    prod.ProductDescription = dataReader["ProductDescription"].ToString();
                    prod.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    prod.SubCategoryId = Convert.ToInt32(dataReader["SubCategoryId"]);
                    prod.Price = Double.Parse(dataReader["Price"].ToString());
                    prod.UnitsInStock = Convert.ToInt32(dataReader["UnitsInStock"]);
                    prod.StockAvailability = Convert.ToInt32(dataReader["StockAvailability"]);
                    prod.Colour = dataReader["Colour"].ToString();
                    prod.Size = dataReader["Size"].ToString();
                    prod.Picture = dataReader["Picture"].ToString();
                }
            }

            DbCommand cmdObj2 = _db.GetStoredProcCommand("GetCategoryNameById");
            _db.AddInParameter(cmdObj2, "@CategoryId", DbType.Int32, Convert.ToInt32(prod.CategoryId));
            _db.AddInParameter(cmdObj2, "@SubCategoryId", DbType.Int32, Convert.ToInt32(prod.SubCategoryId));
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj2))
            {
                while (dataReader.Read())
                {
                    cat.CategoryName = dataReader["CategoryName"].ToString();
                    cat.SubCategoryName = dataReader["SubCategoryName"].ToString();
                }
            }
            pc.product = prod;
            pc.categories = cat;

            return pc;
        }
        //_____________________________________________________________________

        /// <summary>
        /// This method gets all the products in a particular category.
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        
        public static List<Product> GetProductsByCategoryName(string CategoryName)
        {
            var prodlist = new List<Product>();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetProductsByCategory");
            _db.AddInParameter(cmdObj, "@CategoryName", DbType.String, CategoryName);
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    var prod = new Product();
                    prod.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    prod.ProductName = dataReader["ProductName"].ToString();
                    prod.ProductDescription = dataReader["ProductDescription"].ToString();
                    prod.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    prod.SubCategoryId = Convert.ToInt32(dataReader["SubCategoryId"]);
                    prod.Price = Convert.ToInt64(dataReader["Price"]);
                    prod.UnitsInStock = Convert.ToInt32(dataReader["UnitsInStock"]);
                    prod.StockAvailability = Convert.ToInt32(dataReader["StockAvailability"]);
                    prod.Colour = dataReader["Colour"].ToString();
                    prod.Size = dataReader["Size"].ToString();
                    prod.Picture = dataReader["Picture"].ToString();

                    prodlist.Add(prod);
                }
                return prodlist;
            }

        }
        //_______________________________________________________________________

        /// <summary>
        /// This method gets all the products in a particular subcategory.
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <param name="SubCategoryName"></param>
        /// <returns></returns>
        
        public static List<Product> GetProductsBySubCategoryName(string CategoryName, string SubCategoryName)
        {
            var prodlist = new List<Product>();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetProductsBySubCategory");
            _db.AddInParameter(cmdObj, "@CategoryName", DbType.String, CategoryName);
            _db.AddInParameter(cmdObj, "@SubCategoryName", DbType.String, SubCategoryName);
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    var prod = new Product();
                    prod.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    prod.ProductName = dataReader["ProductName"].ToString();
                    prod.ProductDescription = dataReader["ProductDescription"].ToString();
                    prod.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    prod.SubCategoryId = Convert.ToInt32(dataReader["SubCategoryId"]);
                    prod.Price = Convert.ToInt64(dataReader["Price"]);
                    prod.UnitsInStock = Convert.ToInt32(dataReader["UnitsInStock"]);
                    prod.StockAvailability = Convert.ToInt32(dataReader["StockAvailability"]);
                    prod.Colour = dataReader["Colour"].ToString();
                    prod.Size = dataReader["Size"].ToString();
                    prod.Picture = dataReader["Picture"].ToString();

                    prodlist.Add(prod);
                }
                return prodlist;
            }

        }
        //_______________________________________________________________________

        /// <summary>
        /// This method returns a list of all the different categories in the database.
        /// </summary>
        /// <returns></returns>
        
        public static List<string> GetAllCategories()
        {
            var catnames = new List<string>();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetAllCategories");
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    var cat = new Categories();
                    cat.CategoryName = dataReader["CategoryName"].ToString();
                    catnames.Add(cat.CategoryName);
                }
                return catnames;
            }
        }
        //_______________________________________________________________________

        /// <summary>
        /// This method is used by a registered user to update his information (such as default shipping address, card no. etc).
        /// </summary>
        /// <param name="ud"></param>
        /// <returns></returns>
        
        public static string UpdateUser(UserData ud)
        {
            try
            {
                Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
                DbCommand CmdObj = _db.GetStoredProcCommand("UpdateUser");

                _db.AddInParameter(CmdObj, "@UserId", DbType.String, ud.UserId);
                _db.AddInParameter(CmdObj, "@EmailId", DbType.String, ud.EmailId);
                _db.AddInParameter(CmdObj, "@Password", DbType.String, ud.Password);
                _db.AddInParameter(CmdObj, "@FirstName", DbType.String, ud.FirstName);
                _db.AddInParameter(CmdObj, "@LastName", DbType.String, ud.LastName);
                _db.AddInParameter(CmdObj, "@SecurityQuestion", DbType.String, ud.SecurityQuestion);
                _db.AddInParameter(CmdObj, "@SecurityAnswer", DbType.String, ud.SecurityAnswer);
                _db.AddInParameter(CmdObj, "@BillingAddress", DbType.String, ud.BillingAddress);
                _db.AddInParameter(CmdObj, "@ShippingAddress", DbType.String, ud.ShippingAddress);
                _db.AddInParameter(CmdObj, "@CardNumber", DbType.Int64, ud.CardNumber);
                _db.AddInParameter(CmdObj, "@CardType", DbType.String, ud.CardType);
                _db.AddInParameter(CmdObj, "@CardExpiryDate", DbType.DateTime, ud.CardExpiryDate);
                _db.AddInParameter(CmdObj, "@PhoneNumber", DbType.Int64, ud.PhoneNumber);

                _db.AddOutParameter(CmdObj, "@strMessage", DbType.String, 255);
                _db.ExecuteNonQuery(CmdObj);

                return _db.GetParameterValue(CmdObj, "@strMessage").ToString();
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Policy");
                if (rethrow) throw;
                string result = " Error in updating user.";
                return result;
            }

        }
        //_______________________________________________________________________

        /// <summary>
        /// This method is used to fetch all the information from the cart and the user, during the placing of an order, and saves the order in the database. 
        /// </summary>
        /// <param name="userupdate"></param>
        /// <returns></returns>

        public static List<string> PlaceOrder(CheckOutModel userupdate)
        {
            List<string> ls = new List<string>();
            try
            {
                string orderstatus;
                int newOrderId;

                using (TransactionScope ts = new TransactionScope())
                {

                    Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
                    DbCommand cmdObj = _db.GetStoredProcCommand("PlaceOrder");
                    _db.AddInParameter(cmdObj, "@UserId", DbType.String, userupdate.userdata.UserId);
                    _db.AddInParameter(cmdObj, "@EmailId", DbType.String, userupdate.userdata.EmailId);
                    _db.AddInParameter(cmdObj, "@ShippingAddress", DbType.String, userupdate.userdata.ShippingAddress);
                    _db.AddInParameter(cmdObj, "@BillingAddress", DbType.String, userupdate.userdata.BillingAddress);
                    _db.AddInParameter(cmdObj, "@PhoneNumber", DbType.Int64, Convert.ToInt64(userupdate.userdata.PhoneNumber));
                    _db.AddInParameter(cmdObj, "@OrderDate", DbType.DateTime, DateTime.Now);
                    _db.AddInParameter(cmdObj, "@PaymentDate", DbType.DateTime, DateTime.Now);
                    _db.AddInParameter(cmdObj, "@ShippingDate", DbType.DateTime, DateTime.Now.AddDays(2)); // 2 days after placing the order
                    _db.AddInParameter(cmdObj, "@TotalAmount", DbType.Double, userupdate.Total);
                    _db.AddOutParameter(cmdObj, "@strMessage", DbType.String, 255);
                    _db.AddOutParameter(cmdObj, "@getOrderId", DbType.Int32, 255);

                    _db.ExecuteNonQuery(cmdObj);
                    orderstatus = _db.GetParameterValue(cmdObj, "@strMessage").ToString();
                    newOrderId = Int32.Parse(_db.GetParameterValue(cmdObj, "@getOrderId").ToString());

                    List<AddToCartModel> cartdata = ShoppingCartImplementation.GetFromCart(userupdate.userdata.UserId);

                    try
                    {
                        foreach (var item in cartdata)
                        {
                            DbCommand cmdObj2 = _db.GetStoredProcCommand("ProductsInOrder");
                            _db.AddInParameter(cmdObj2, "@getOrderId", DbType.Int32, newOrderId);
                            _db.AddInParameter(cmdObj2, "@ProductId", DbType.Int32, item.Product.ProductId);
                            _db.AddInParameter(cmdObj2, "@Price", DbType.Double, item.Product.Price);
                            _db.AddInParameter(cmdObj2, "@Quantity", DbType.Int32, item.Cart.Quantity);
                            _db.AddOutParameter(cmdObj2, "@strMessage", DbType.String, 255);
                            _db.ExecuteNonQuery(cmdObj2);
                            string status = _db.GetParameterValue(cmdObj2, "@strMessage").ToString();
                        }
                        ts.Complete();
                        ls.Add(orderstatus);
                        return ls;
                    }
                    catch
                    {
                        //Rollback
                    }
                    if (ls.Count == 0)
                    {

                        //LetsShopImplementation.CancelMyOrder(newOrderId, userupdate.userdata.UserId);
                        for (int i = 0; i < cartdata.Count; i++)
                        {
                            Database db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
                            DbCommand _cmdObj = db.GetStoredProcCommand("RemainingProducts");
                            db.AddInParameter(_cmdObj, "@ProductId", DbType.Int32, cartdata[i].Product.ProductId);
                            db.AddInParameter(_cmdObj, "@Quantity", DbType.Int32, cartdata[i].Cart.Quantity);
                            db.AddOutParameter(_cmdObj, "@strMessages", DbType.String, 255);
                            db.ExecuteNonQuery(_cmdObj);
                            string remain = db.GetParameterValue(_cmdObj, "@strMessages").ToString();
                            if (remain != "")
                            {
                                ls.Add(remain);
                            }
                        }
                        return ls;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Policy");
                if (rethrow) throw;
                string result = " Error in placing an order.";
                ls.Add(result);
                return ls;
            }

        }

        //_____________________________________________________________________

        /// <summary>
        /// This method is used to get full information of a particular order, and helps an user track his order.
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        
        public static OrderInformation GetOrderDetails(int orderid)
        {

            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetOrderDetails1");  // for the list of products
            _db.AddInParameter(cmdObj, "@OrderId", DbType.Int32, orderid);

            var orderslist = new List<OrderDetails>();
          
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    OrderDetails od = new OrderDetails();
                    Product prod = new Product();
                    od.quantity = Convert.ToInt32(dataReader["Quantity"]);
                    prod.ProductId = Convert.ToInt32(dataReader["ProductId"]);

                    DbCommand cmdObj2 = _db.GetStoredProcCommand("GetProductById");
                    _db.AddInParameter(cmdObj2, "@ProductId", DbType.Int32, Convert.ToInt32(prod.ProductId));

                    using (IDataReader dataReader2 = _db.ExecuteReader(cmdObj2))
                    {
                        while (dataReader2.Read())
                        {
                            prod.ProductName = dataReader2["ProductName"].ToString();
                            prod.Price = Double.Parse(dataReader2["Price"].ToString());
                        }
                    }
                    od.product = prod;
                    orderslist.Add(od);
                }
            }

            OrderInformation oi = new OrderInformation();
            Orders ord = new Orders();

            DbCommand cmdObj3 = _db.GetStoredProcCommand("GetOrderDetails2");  // for the user's information
            _db.AddInParameter(cmdObj3, "@OrderId", DbType.Int32, orderid);

            using (IDataReader dataReader3 = _db.ExecuteReader(cmdObj3))
            {
                while (dataReader3.Read())
                {
                    ord.OrderId = Convert.ToInt32(dataReader3["OrderId"].ToString());
                    ord.UserId = dataReader3["UserId"].ToString();
                    ord.OrderDate = Convert.ToDateTime(dataReader3["OrderDate"]);
                    ord.ShippingDate = Convert.ToDateTime(dataReader3["ShippingDate"]);
                    ord.TransactionStatus = dataReader3["TransactionStatus"].ToString();
                    ord.PaymentDate = Convert.ToDateTime(dataReader3["PaymentDate"]);
                    ord.TotalAmount = Convert.ToDouble(dataReader3["TotalAmount"]);
                    ord.BillingAddress = dataReader3["BillingAddress"].ToString();
                    ord.ShippingAddress = dataReader3["ShippingAddress"].ToString();
                    ord.PhoneNumber = Convert.ToInt64(dataReader3["PhoneNumber"]);
                    ord.EmailId = dataReader3["EmailId"].ToString();

                }
            }

            oi.orderproduct = orderslist;
            oi.orderuser = ord;

            return oi;

        }
        //_____________________________________________________________________

        /// <summary>
        /// An administrator can use this method to get all the orders registered in the website.
        /// </summary>
        /// <returns></returns>
        
        public static List<Orders> GetAllOrders()
        {
            var orderslist = new List<Orders>();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetOrders");
            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    var order = new Orders();

                    order.UserId = dataReader["UserId"].ToString();
                    order.TransactionStatus = dataReader["TransactionStatus"].ToString();
                    order.EmailId = dataReader["EmailId"].ToString();

                    int orderId;
                    int.TryParse(dataReader["OrderId"].ToString(), out orderId);
                    order.OrderId = orderId;

                    long phoneNo;
                    long.TryParse(dataReader["PhoneNumber"].ToString(), out phoneNo);
                    order.PhoneNumber = phoneNo;

                    DateTime orderDate;
                    DateTime.TryParse(dataReader["OrderDate"].ToString(), out orderDate);
                    order.OrderDate = orderDate;

                    orderslist.Add(order);
                }
                return orderslist;
            }
        }
        //_____________________________________________________________________

        /// <summary>
        /// A registered user can use this method to get the history of all his orders.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        
        public static List<Orders> GetMyOrders(string userId)
        {
            var orderslist = new List<Orders>();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");

            DbCommand cmdObj = _db.GetStoredProcCommand("GetMyOrders");
            _db.AddInParameter(cmdObj, "@UserId", DbType.String, userId);
            _db.ExecuteNonQuery(cmdObj);

            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    var order = new Orders();

                    order.UserId = dataReader["UserId"].ToString();

                    int orderId;
                    int.TryParse(dataReader["OrderId"].ToString(), out orderId);
                    order.OrderId = orderId;

                    DateTime dt = Convert.ToDateTime(dataReader["ShippingDate"]);
                    if ( dt <= DateTime.Now)
                    {
                        order.TransactionStatus = "Shipped";
                        DbCommand cmdObj2 = _db.GetStoredProcCommand("UpdateTransactionStatus");
                        _db.AddInParameter(cmdObj2, "@OrderId", DbType.Int32, order.OrderId);
                        _db.ExecuteNonQuery(cmdObj2);
                    }
                    else
                    {
                        order.TransactionStatus = dataReader["TransactionStatus"].ToString();
                    }
                    
                    order.EmailId = dataReader["EmailId"].ToString();

                    long phoneNo;
                    long.TryParse(dataReader["PhoneNumber"].ToString(), out phoneNo);
                    order.PhoneNumber = phoneNo;

                    DateTime orderDate;
                    DateTime.TryParse(dataReader["OrderDate"].ToString(), out orderDate);
                    order.OrderDate = orderDate;

                    orderslist.Add(order);
                }
                return orderslist;
            }
        }
        //_____________________________________________________________________

        /// <summary>
        /// Any registered user uses this method to cancel his order after placing, provided it is not yet shipped. 
        /// </summary>
        /// <returns></returns>

        public static string CancelMyOrder(int orderid, string userid)
        {
            try
            {
                string cancelstatus;

                Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
                DbCommand cmdObj = _db.GetStoredProcCommand("CancelOrder");
                _db.AddInParameter(cmdObj, "@UserId", DbType.String, userid);
                _db.AddInParameter(cmdObj, "@OrderId", DbType.Int32, Convert.ToInt32(orderid));
                _db.AddOutParameter(cmdObj, "@strMessage", DbType.String, 255);
                _db.ExecuteNonQuery(cmdObj);

                cancelstatus = _db.GetParameterValue(cmdObj, "@strMessage").ToString();
                return cancelstatus;
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Policy");
                if (rethrow) throw;
                string result = " Error in canceling the order.";
                return result;
            }
        }
        //_____________________________________________________________________

        /// <summary>
        /// This method filters the price of the items between a specified range.
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="Max"></param>
        /// <param name="Min"></param>
        /// <returns></returns>

        public static List<Product> PriceFilter(string CategoryName, int Max, int Min)
        {
            var prodlist = new List<Product>();
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("PriceFilter");
            _db.AddInParameter(cmdObj, "@CategoryName", DbType.String, CategoryName);
            _db.AddInParameter(cmdObj, "@Max", DbType.Int32, Convert.ToInt32(Max));
            _db.AddInParameter(cmdObj, "@Min", DbType.Int32, Convert.ToInt32(Min));
            _db.AddOutParameter(cmdObj, "@Message", DbType.String, 255);

            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    var prod = new Product();
                    prod.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    prod.ProductName = dataReader["ProductName"].ToString();
                    prod.ProductDescription = dataReader["ProductDescription"].ToString();
                    prod.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    prod.SubCategoryId = Convert.ToInt32(dataReader["SubCategoryId"]);
                    prod.Price = Convert.ToInt64(dataReader["Price"]);
                    prod.UnitsInStock = Convert.ToInt32(dataReader["UnitsInStock"]);
                    prod.StockAvailability = Convert.ToInt32(dataReader["StockAvailability"]);
                    prod.Colour = dataReader["Colour"].ToString();
                    prod.Size = dataReader["Size"].ToString();
                    prod.Picture = dataReader["Picture"].ToString();

                    prodlist.Add(prod);
                }
                return prodlist;
            }
        }
        //_____________________________________________________________________

        /// <summary>
        /// This method returns the corresponding category id from the category name.
        /// </summary>
        /// <param name="catname"></param>
        /// <param name="subcatname"></param>
        /// <returns></returns>
        
        public static Categories GetCategoryIdByName(string catname, string subcatname)
        {
            Categories catobj = new Categories();

            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetCategoryIdByName");
            _db.AddInParameter(cmdObj, "@CategoryName", DbType.String, catname);
            _db.AddInParameter(cmdObj, "@SubCategoryName", DbType.String, subcatname);

            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    catobj.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                    catobj.SubCategoryId = Convert.ToInt32(dataReader["SubCategoryId"]);
                    
                }
                return catobj;
            }
        }

        //_____________________________________________________________________

        /// <summary>
        /// This method is used by an administrator to change the order status from 'Processing' to 'Shipped'.
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="UserId"></param>
        /// <param name="TransactionStatus"></param>
        /// <returns></returns>
        
        public static string OrderStatus(int orderid, string UserId, string TransactionStatus)
        {

            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("ChangeStatus");
            _db.AddInParameter(cmdObj, "@UserId", DbType.String, UserId);
            _db.AddInParameter(cmdObj, "@OrderId", DbType.Int32, orderid);
            _db.AddInParameter(cmdObj, "@TransactionStatus", DbType.String, TransactionStatus);
            _db.AddInParameter(cmdObj, "@ShippingDate", DbType.DateTime, DateTime.Now);
            _db.AddOutParameter(cmdObj, "@strMessage", DbType.String, 255);
            _db.ExecuteNonQuery(cmdObj);
            string result = _db.GetParameterValue(cmdObj, "@strMessage").ToString();
            return result;
        }

        //_____________________________________________________________________

        /// <summary>
        /// This method is used during login by a security answer, when a user forgets his/her password.
        /// </summary>
        /// <param name="UserIds"></param>
        /// <param name="SecurityAnswer"></param>
        /// <returns></returns>

        public static UserCredentials LoginAfterForgotPassword(string UserIds, string SecurityAnswer)
        {
            UserData userdata = new UserData();
            string SecurityQuestion = userdata.SecurityQuestion;
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("LoginAfterForgotPassword");
            _db.AddInParameter(cmdObj, "@UserId", DbType.String, UserIds);
            _db.AddInParameter(cmdObj, "@SecurityQuestion", DbType.String, SecurityQuestion);
            _db.AddInParameter(cmdObj, "@SecurityAnswer", DbType.String, SecurityAnswer);
            _db.AddOutParameter(cmdObj, "@strMessage", DbType.String, 255);
            _db.ExecuteNonQuery(cmdObj);
            string result = _db.GetParameterValue(cmdObj, "@strMessage").ToString();
            UserCredentials use1 = new UserCredentials();
            use1.UserId = UserIds;
            use1.Password = result;

            return use1;

        }

    }

}
