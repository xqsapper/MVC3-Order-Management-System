using System;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataClassLibrary;

namespace Mvc3LetsShopProject.Controllers
{
    /// <summary>
    /// This controller is used for accessing and performing all the methods related to an order.
    /// </summary>

    public class OrdersController : Controller
    {
        /// <summary>
        /// GET: /Orders/
        /// </summary>
        /// <returns></returns>

        public ActionResult Index()
        {
            if (Session["usertype"] != null)   // If some user is logged in
            {
                if (Session["usertype"].ToString() == "Admin")
                {
                    return View(LetsShopImplementation.GetAllOrders());
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
        /// GET: /Orders/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        public ActionResult Details(int id)
        {
            return View();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// GET: /Orders/Create
        /// </summary>
        /// <returns></returns>

        public ActionResult Create()
        {
            return View();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /Orders/Create
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult Create(FormCollection collection)
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
        /// GET: /Orders/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /Orders/Edit/5
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
        /// GET: /Orders/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //______________________________________________________________________________________

        /// <summary>
        /// POST: /Orders/Delete/5
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
        /// GET: /Orders/ExportAllOrders [Exports all users to an Excel File]
        /// </summary>
        /// <returns></returns>

        public ActionResult ExportAllOrders()
        {
            try
            {
                GridView gv = new GridView();
                gv.DataSource = LetsShopImplementation.GetAllOrders();
                gv.DataBind();
                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachment; filename=AllOrders.xls");
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
        /// This method is used to save the transaction status which has been changed from 'Processing' to 'Shipped'.
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="UserId"></param>
        /// <param name="TransactionStatus"></param>
        /// <returns></returns>

        public ActionResult SaveChanges(int OrderId, string UserId, string TransactionStatus)
        {
            if (TransactionStatus == "Shipped")
            {
                LetsShopImplementation.OrderStatus(OrderId, UserId, TransactionStatus);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //______________________________________________________________________________________
    }
}
