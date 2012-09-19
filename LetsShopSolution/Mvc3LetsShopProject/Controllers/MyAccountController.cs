using System;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataClassLibrary;
using DataClassLibrary.Models;

namespace Mvc3LetsShopProject.Controllers
{
    /// <summary>
    /// This controller is used for storing all the details related to a particular user (eg - his details, orders etc.)
    /// </summary>

    public class MyAccountController : Controller
    {
        /// <summary>
        /// GET: /MyAccount/
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            if (Session["usertype"] != null)   // If some user is logged in
            {
                string LoggedInUser = @User.Identity.Name; // the string "LoggedInUser" wil contain the username of the user who logs in!
                return View(LetsShopImplementation.GetUserDetailsByUserName(LoggedInUser));
            }
            else
                return RedirectToAction("Index", "Home");
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /MyAccount/Details/5
        /// </summary>
        /// <returns></returns>

        public ActionResult Details()
        {
            return View(LetsShopImplementation.GetUserDetailsByUserName(@User.Identity.Name));
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /MyAccount/Edit
        /// </summary>
        /// <returns></returns>
 
        public ActionResult Edit()
        {
            string LoggedInUser = @User.Identity.Name;
            return View(LetsShopImplementation.GetUserDetailsByUserName(LoggedInUser));
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /MyAccount/Edit/5
        /// </summary>
        /// <param name="user"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult Edit(UserData user, FormCollection collection)
        {
            try
            {
                string UserId = user.UserId;
                string EmailId = user.EmailId;
                string Password = user.Password;
                string FirstName = user.FirstName;
                string LastName = user.LastName;
                string BillingAddress = user.BillingAddress;
                string ShippingAddress = user.ShippingAddress;
                long CardNumber = user.CardNumber;
                string CardType = user.CardType;
                DateTime CardExpiryDate = user.CardExpiryDate;
                long PhoneNumber = user.PhoneNumber;

                LetsShopImplementation.UpdateUser(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: MyAccount/MyOrders
        /// </summary>
        /// <returns></returns>

        public ActionResult MyOrders()
        {
            if (Session["usertype"] != null)
            {
                return View(LetsShopImplementation.GetMyOrders(@User.Identity.Name));
            }
            else
                return RedirectToAction("Index", "Home");
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: MyAccount/CancelOrder/5
        /// </summary>
        /// <returns></returns>

        public ActionResult CancelOrder(int id = 0)
        {
            if (Session["usertype"] != null)
            {
                return View(LetsShopImplementation.GetOrderDetails(id));
            }
            else
                return RedirectToAction("Index", "Home");
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: MyAccount/CancelOrder/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        
        [HttpPost]
        public ActionResult CancelOrder(int id, string userid)
        {
            if (Session["usertype"] != null)
            {
                string result = LetsShopImplementation.CancelMyOrder(id, @User.Identity.Name);
                return RedirectToAction("MyOrders", "MyAccount");
            }
            else
                return RedirectToAction("Index", "Home");
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: MyAccount/OrderDetails/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult OrderDetails(int id = 0)
        {
            if (Session["usertype"] != null)
            {
                return View(LetsShopImplementation.GetOrderDetails(id));
            }
            else
                return RedirectToAction("Index", "Home");
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: MyAccount/ExportMyOrders [Exports all users to an Excel File]
        /// </summary>
        /// <returns></returns>

        public ActionResult ExportMyOrders()
        {
            try
            {
                GridView gv = new GridView();
                gv.DataSource = LetsShopImplementation.GetMyOrders(@User.Identity.Name);
                gv.DataBind();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=MyOrders.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gv.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception)
            {
                return RedirectToAction("ErrorPage", "Product");
            }
            return View("Index");
        }

        //______________________________________________________________________________________

    }
}
