using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Mvc3LetsShopProject.Models.DAO;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace Mvc3LetsShopProject.Models.DTO
{
    public class CachingImplementation
    {
        List<Product> listProducts = null;
         ICacheManager cacheProductData;
         bool CacheFlag = false;
        public CachingImplementation()
        {

        }
        public List<Product> GetProductData(out string cacheStatus)
        {
            cacheProductData = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
            listProducts = (List<Product>)cacheProductData["ProductDataCache"];
            cacheStatus = "Employee Information is Retrived from Cache";
            LetsShopImplementation ls = new LetsShopImplementation();
            CacheFlag = true;
            if (listProducts == null)
            {
              //Database _db = EnterpriseLibraryContainer.Current.GetInstance<Database>("LetsShopConnection");
              //listProducts = 

                LetsShopImplementation ls = new LetsShopImplementation();
                listProducts = ls.GetProducts();
                AbsoluteTime CacheExpirationTime = new AbsoluteTime(new TimeSpan(1, 0, 0));
                cacheProductData.Add("ProductDataCache", listProducts, CacheItemPriority.High, null, new ICacheItemExpiration[] { CacheExpirationTime });
                cacheStatus = "Product Info is Added in cache";
                CacheFlag = false;
            }
            return listProducts;

            }
        public Product GetProductById(int ProductId, out string cacheStatus)
        {

            Product productSelected = null;
            productSelected = (from prod in listProducts where prod.ProductId == ProductId select prod).First();
            cacheStatus = "Product Info is retrived from Cache";
            return productSelected;
        }

        public static void ClearCache(out string cacheStatus)
        {
            ICacheManager cacheProductData = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
            cacheProductData.Flush();
            cacheStatus = "Cache is Cleared";
        }
        }
    
    }
}
