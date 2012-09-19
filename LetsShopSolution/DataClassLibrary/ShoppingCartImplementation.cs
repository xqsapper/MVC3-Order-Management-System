using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DataClassLibrary.Models;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace DataClassLibrary
{
    /// <summary>
    /// This class implements all the functionalities related to cart.
    /// </summary>
    public class ShoppingCartImplementation
    {
        /// <summary>
        /// This method adds a single product to the cart, corresponding to a particular user.
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        
        public static string AddToCart(int ProductId, string UserId)
        {
            string result;
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("AddtoCart");
            _db.AddInParameter(cmdObj, "@ProductId", DbType.Int32, ProductId);
            _db.AddInParameter(cmdObj, "@UserId", DbType.String, UserId);
            _db.AddOutParameter(cmdObj, "@strMessage", DbType.String, 255);
            _db.ExecuteNonQuery(cmdObj);
            result = _db.GetParameterValue(cmdObj, "@strMessage").ToString();
            return result;

        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method reduces the quantity of a particular product in the cart by one, corresponding to a particular user.
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>

        public static string ReduceFromCart(int ProductId, string UserId)
        {
            string result;
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("ReduceFromCart");
            _db.AddInParameter(cmdObj, "@ProductId", DbType.Int32, ProductId);
            _db.AddInParameter(cmdObj, "@UserId", DbType.String, UserId);
            _db.AddOutParameter(cmdObj, "@strMessage", DbType.String, 255);
            _db.ExecuteNonQuery(cmdObj);
            result = _db.GetParameterValue(cmdObj, "@strMessage").ToString();
            return result;
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to get all the products details present in the cart of a particular user.
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        
        public static List<AddToCartModel> GetFromCart(string userid)
        {
            List<AddToCartModel> listCartModel = new List<AddToCartModel>();

            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("GetFromCart");
            _db.AddInParameter(cmdObj, "@UserId", DbType.String, userid);

            using (IDataReader dataReader = _db.ExecuteReader(cmdObj))
            {
                while (dataReader.Read())
                {
                    AddToCartModel cartmodel = new AddToCartModel();
                    Product prod = new Product();
                    Cart cart = new Cart();
                    prod.ProductId = Convert.ToInt32(dataReader["ProductId"]);
                    prod.ProductName = dataReader["ProductName"].ToString();
                    prod.ProductDescription = dataReader["ProductDescription"].ToString();
                    prod.Price = Double.Parse(dataReader["Price"].ToString());
                    cart.Quantity = Convert.ToInt32(dataReader["Quantity"]);
                    cartmodel.Product = prod;
                    cartmodel.Cart = cart;
                    listCartModel.Add(cartmodel);

                }
            }
            return listCartModel;

        }

        //______________________________________________________________________________________

        /// <summary>
        /// This is used to remove an individual product from the cart.
        /// </summary>
        /// <param name="ProductId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        
        public static string RemoveFromCart(int ProductId, string UserId)
        {
            string result;
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("RemoveFromCart");
            _db.AddInParameter(cmdObj, "@ProductId", DbType.Int32, Convert.ToInt32(ProductId));
            _db.AddInParameter(cmdObj, "@UserId", DbType.String, UserId);
            _db.AddOutParameter(cmdObj, "@strMessage", DbType.String, 255);
            _db.ExecuteNonQuery(cmdObj);
            result = _db.GetParameterValue(cmdObj, "@strMessage").ToString();
            return result;
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to remove all the products from the cart.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        
        public static string EmptyCart(string UserId)
        {
            string result;
            Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnString");
            DbCommand cmdObj = _db.GetStoredProcCommand("EmptyCart");
            _db.AddInParameter(cmdObj, "@UserId", DbType.String, UserId);
            _db.AddOutParameter(cmdObj, "@strMessage", DbType.String, 255);
            _db.ExecuteNonQuery(cmdObj);
            result = _db.GetParameterValue(cmdObj, "@strMessage").ToString();
            return result;
        }

        //______________________________________________________________________________________

    }
}