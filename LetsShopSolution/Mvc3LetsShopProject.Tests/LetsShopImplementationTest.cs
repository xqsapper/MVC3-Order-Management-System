//using Mvc3LetsShopProject.Models.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using DataClassLibrary.Models;
using DataClassLibrary;

namespace Mvc3LetsShopProject.Tests
{


    /// <summary>
    ///This is a test class for LetsShopImplementationTest and is intended
    ///to contain all LetsShopImplementationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LetsShopImplementationTest
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
        ///A test for CreateProduct
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]

        public void CreateProductTest()
        {
            Product product = new Product()
            {
                ProductName = "Fridge",
                ProductDescription = "home appliance",
                SubCategoryId = 3,
                Price = 6000,
                Colour = "red",
                Size = "165 lts",
                UnitsInStock = 5,
                StockAvailability = 1,
                Picture = null
            };
            string expected = "Product Created Successfully"; 
            string actual;
            actual = LetsShopImplementation.CreateProduct(product);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DeleteProduct
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]

        public void DeleteProductTest()
        {
            Product product = new Product(); // TODO: Initialize to an appropriate value

            int id = 7;

            string expected = "Product Deleted Successfully"; // TODO: Initialize to an appropriate value
            string actual;
            actual = LetsShopImplementation.DeleteProduct(id);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
        [TestMethod()]
        public void NegativeDeleteProductTest()
        {
            Product product = new Product(); // TODO: Initialize to an appropriate value

            int id = 56;

            string expected = "Product Id doesnot exists"; // TODO: Initialize to an appropriate value
            string actual;
            actual = LetsShopImplementation.DeleteProduct(id);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateProduct
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]

        public void UpdateProductTest()
        {
            Product product = new Product()
            {
                ProductId = 8,
                ProductName = "Fan",
                ProductDescription = "home appliance",
                SubCategoryId = 3,
                Price = 6000,
                //QuantityPerUnit = 4,
                Colour = "red",
                Size = "165 lts",
                //Discount = 500,
                UnitsInStock = 5,
                StockAvailability = 1,
                Picture = null
            }; // TODO: Initialize to an appropriate value
            string expected = "Product Updated Successfully"; // TODO: Initialize to an appropriate value
            string actual;
            actual = LetsShopImplementation.UpdateProduct(product);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
        [TestMethod()]
        public void NegativeUpdateProductTest()
        {
            Product product = new Product()
            {
                ProductId = 25,
                ProductName = "Fan",
                ProductDescription = "home appliance",
                SubCategoryId = 3,
                Price = 6000,
                //QuantityPerUnit = 4,
                Colour = "red",
                Size = "165 lts",
                //Discount = 500,
                UnitsInStock = 5,
                StockAvailability = 1,
                Picture = null
            }; // TODO: Initialize to an appropriate value
            string expected = "Error in Updating Product"; // TODO: Initialize to an appropriate value
            string actual;
            actual = LetsShopImplementation.UpdateProduct(product);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateUser
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]

        public void CreateUserTest()
        {
            string UserId = string.Empty; // TODO: Initialize to an appropriate value
            string Password = string.Empty; // TODO: Initialize to an appropriate value
            string EmailId = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = LetsShopImplementation.CreateUser(UserId, Password, EmailId);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
