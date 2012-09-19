using DataClassLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
//using Mvc3LetsShopProject.Models.DTO;
using System.Collections.Generic;
using DataClassLibrary;

namespace Mvc3LetsShopProject.Tests
{


    /// <summary>
    ///This is a test class for ShoppingCartImplementationTest and is intended
    ///to contain all ShoppingCartImplementationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ShoppingCartImplementationTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AddToCart
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        public int GetQuantityFromId(int id, List<AddToCartModel> lst)
        {
            int quantity = 0;
            for (int i = 0; i < lst.Count; i++)
            {
                if (lst[i].Product.ProductId == id)
                    quantity = lst[i].Cart.Quantity;
            }
            return quantity;
        }



        [TestMethod()]
        //TESTING ITEMS ADDED TO CART WHEN THE QUANTITY INCREASES.
        public void AddToCartTest()
        {
            string UserId = "ashaka";
            List<AddToCartModel> list = new List<AddToCartModel>();
            list = ShoppingCartImplementation.GetFromCart(UserId);
            int pid = 5;
            int quantity = GetQuantityFromId(pid, list);
            string actual;
            actual = ShoppingCartImplementation.AddToCart(pid, UserId);
            List<AddToCartModel> list1 = new List<AddToCartModel>();
            list1 = ShoppingCartImplementation.GetFromCart(UserId);
            int x = GetQuantityFromId(pid, list1);

            Assert.AreEqual(x, quantity + 1);
        }



        /// <summary>
        ///A test for RemoveFromCart
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void RemoveFromCartTest()
        {
            string UserId = "ashaka";
            List<AddToCartModel> list = new List<AddToCartModel>();
            list = ShoppingCartImplementation.GetFromCart(UserId);
            int x = list.Count;
            int id = 5;
            string actual;
            actual = ShoppingCartImplementation.RemoveFromCart(id, UserId);
            List<AddToCartModel> list1 = new List<AddToCartModel>();
            list1 = ShoppingCartImplementation.GetFromCart(UserId);
            int y = list1.Count;

            Assert.AreEqual(y + 1, x);
        }
    }


}
