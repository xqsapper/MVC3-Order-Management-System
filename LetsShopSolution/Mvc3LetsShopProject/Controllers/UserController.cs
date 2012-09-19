using System;
using System.IO;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataClassLibrary;
using DataClassLibrary.Models;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Mvc3LetsShopProject.Controllers
{
    /// <summary>
    /// This controller is used for accessing and performing all the methods related to a user.
    /// </summary>

    public class UserController : Controller
    {
        
        /// <summary>
        /// GET: /User/
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            if (Session["usertype"] != null)   // If some user is logged in
            {
                if (Session["usertype"].ToString() == "Admin")
                {
                    return View(LetsShopImplementation.GetAllUsers());
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
        /// GET: /User/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ViewResult Details(int id)
        {
            return View(LetsShopImplementation.GetUserDetailsById(id));
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /User/Create
        /// </summary>
        /// <returns></returns>

        public ActionResult SignUp()
        {
            return View();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /User/SignUp
        /// </summary>
        /// <param name="UserData"></param>
        /// <param name="collection"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult SignUp(UserData UserData, FormCollection collection)
        {
            try
            {
                string FirstName = UserData.FirstName;
                string LastName = UserData.LastName;
                string UserId = UserData.UserId;
                string Password = UserData.Password;
                string BillingAddress = UserData.BillingAddress;
                string ShippingAddress = UserData.ShippingAddress;
                double CardNumber = UserData.CardNumber;
                string CardType = UserData.CardType;
                DateTime CardExpiryDate = UserData.CardExpiryDate;
                double PhoneNumber = UserData.PhoneNumber;
                string EmailId = UserData.EmailId;

                ViewBag.SignUpMessage = LetsShopImplementation.CreateUser(UserId, Password, EmailId);
                
                return View();
            }
            catch(Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "UI Policy");
                if (rethrow) throw;
                return RedirectToAction("ErrorPage", "Product");
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /User/Login
        /// </summary>
        /// <returns></returns>

        public string Login()
        {
            return "get method";
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /User/Login
        /// </summary>
        /// <param name="UserCredentials"></param>
        /// <param name="collection"></param>
        /// <returns></returns>

        [HttpPost]
        public string Login(UserCredentials UserCredentials, FormCollection collection)
        {
            try
            {
                ViewBag.LoginMessage = LetsShopImplementation.ValidateUser(UserCredentials.UserId, UserCredentials.Password);
                if ((ViewBag.LoginMessage == "Admin logged in successfully") || (ViewBag.LoginMessage == "Guest logged in successfully"))
                {
                    string type = ViewBag.LoginMessage;
                    type = type.Split(' ')[0];
                    Session["usertype"] = type;
                    FormsAuthentication.SetAuthCookie(UserCredentials.UserId, false);
                }
                return ViewBag.LoginMessage;
            }
            catch
            {
                return "The network is down. Please try some time later.";
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /User/LogOff
        /// </summary>
        /// <returns></returns>

        public ActionResult LogOff()
        {
            Session["usertype"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /User/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /User/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /User/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /User/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //______________________________________________________________________________________
        
        /// <summary>
        /// GET: /User/ExportAllUsers [Exports all users to an Excel File]
        /// </summary>
        /// <returns></returns>

        public ActionResult ExportAllUsers()
        {
            try
            {
                GridView gv = new GridView();
                gv.DataSource = LetsShopImplementation.GetAllUsers();
                gv.DataBind();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=RegisteredUsers.xls");
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

        /// <summary>
        /// This method is used to prompt the user for his userid, before proceeding to retrieve his security question.
        /// </summary>
        /// <returns></returns>

        public ActionResult ForgotPassword()
        {
            return View();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to redirect to the page: User/SecurityQuestion, after confirmation of the userid.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult ForgotPassword(string UserId)
        {
            return RedirectToAction("SecurityQuestion", "User", new { id = UserId });
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method is used to display the security question so that the user can login with his security answer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult SecurityQuestion(string id)
        {
            return View(LetsShopImplementation.ForgotPassword(id));
        }

        //______________________________________________________________________________________

        /// <summary>
        /// This method signs in a user with the help of his security answer.
        /// </summary>
        /// <param name="userdata"></param>
        /// <param name="fc1"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult SecurityQuestion(UserData userdata, FormCollection fc1)
        {
            UserCredentials usercr = new UserCredentials();
            usercr = LetsShopImplementation.LoginAfterForgotPassword(userdata.UserId, userdata.SecurityAnswer);
            if (usercr.Password == "")
            {
                ViewBag.message = "Security answer does not match";
                return View(LetsShopImplementation.ForgotPassword(userdata.UserId));
            }
            else
            {
                Login(usercr, fc1);
                return RedirectToAction("Index", "Home");
            }

        }

        //______________________________________________________________________________________

    }
}
