using Mvc3LetsShopProject.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;
using DataClassLibrary.Models;
using System.Web;
using System.Web.Routing;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace Mvc3LetsShopProject.Tests
{
    
    
    /// <summary>
    ///This is a test class for ProductControllerTest and is intended
    ///to contain all ProductControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProductControllerTest
    {

        public ProductControllerTest()
        {
            // create some mock products to play with
            IList<Product> products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Television",
                ProductDescription = "Sony", Price = 25000 },
            new Product { ProductId = 2, ProductName = "Computer",
                ProductDescription = "Dell", Price = 20000 },
            new Product { ProductId = 4, ProductName = "Table",
                ProductDescription = "Wooden", Price = 600 }
        };

            // Mock the Products Repository using Moq
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();

            // Return all the products
            mockProductRepository.Setup(mr => mr.FindAll()).Returns(products);

            // return a product by Id

            mockProductRepository.Setup(mr => mr.FindById(It.IsAny<int>())).Returns((int i) => products.Where(x => x.ProductId == i).Single());

            // return a product by Name
            mockProductRepository.Setup(mr => mr.FindByName(It.IsAny<string>())).Returns((string s) => products.Where(x => x.ProductName == s).Single());

            // Allows us to test saving a product
            mockProductRepository.Setup(mr => mr.Save(It.IsAny<Product>())).Returns(
               (Product target) =>
               {
                   if (target.ProductId.Equals(default(int)))
                   {
                       target.ProductId = products.Max(q => q.ProductId) + 1;
                       //target.ProductId = products.Count() + 1;
                       products.Add(target);
                   }
                   else
                   {
                       var original = products.Where(q => q.ProductId == target.ProductId).SingleOrDefault();

                       if (original != null)
                       {
                           return false;
                       }

                       products.Add(target);
                   }

                   return true;
               });

            // Complete the setup of our Mock Product Repository
            this.MockProductsRepository = mockProductRepository.Object;
        }
        public readonly IProductRepository MockProductsRepository;
        //public TestContext TestContext { get; set; }

        [TestMethod]
        public void CanReturnProductById()
        {
            // Try finding a product by id
            Product testProduct = this.MockProductsRepository.FindById(2);

            Assert.IsNotNull(testProduct); // Test if null
            Assert.IsInstanceOfType(testProduct, typeof(Product)); // Test type
            Assert.AreEqual("Computer", testProduct.ProductName); // Verify it is the right product
        }

        /// <summary>
        /// Can we return a product By Name?
        /// </summary>
        [TestMethod]
        public void CanReturnProductByName()
        {
            // Try finding a product by Name
            Product testProduct = this.MockProductsRepository.FindByName("Computer");

            Assert.IsNotNull(testProduct); // Test if null
            Assert.IsInstanceOfType(testProduct, typeof(Product)); // Test type
            Assert.AreEqual(2, testProduct.ProductId); // Verify it is the right product
        }

        /// <summary>
        /// Can we return all products?
        /// </summary>
        [TestMethod]
        public void CanReturnAllProducts()
        {
            // Try finding all products
            IList<Product> testProducts = this.MockProductsRepository.FindAll();

            Assert.IsNotNull(testProducts); // Test if null
            Assert.AreEqual(3, testProducts.Count); // Verify the correct Number
        }

        [TestMethod]
        public void CanInsertProduct()
        {
            // Create a new product, not I do not supply an id
            Product newProduct = new Product { ProductName = "asp.net MVC3", ProductDescription = "Book", Price = 399.99 };

            int productCount = this.MockProductsRepository.FindAll().Count;
            Assert.AreEqual(3, productCount); // Verify the expected Number pre-insert

            // try saving our new product
            this.MockProductsRepository.Save(newProduct);

            // demand a recount
            productCount = this.MockProductsRepository.FindAll().Count;
            Assert.AreEqual(4, productCount); // Verify the expected Number post-insert

            // verify that our new product has been saved
            Product testProduct = this.MockProductsRepository.FindByName("asp.net MVC3");
            Assert.IsNotNull(testProduct); // Test if null
            Assert.IsInstanceOfType(testProduct, typeof(Product)); // Test type
            Assert.AreEqual(5, testProduct.ProductId); // Verify it has the expected productid
        }

        [TestMethod]
        public void CanUpdateProduct()
        {
            // Find a product by id
            Product testProduct = this.MockProductsRepository.FindById(1);

            // Change one of its properties
            testProduct.ProductName = "TV";

            // Save our changes.
            this.MockProductsRepository.Save(testProduct);

            // Verify the change
            Assert.AreEqual("TV", this.MockProductsRepository.FindById(1).ProductName);
        }

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

    }
}
