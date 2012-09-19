using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataClassLibrary.Models
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public string TransactionStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public double TotalAmount { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public long PhoneNumber { get; set; }
        public string EmailId { get; set; }

        public IEnumerable<SelectListItem> StatusList
        {
            get
            {
                return
                    new SelectListItem[]
                    {   
                        new SelectListItem { Value = "Processing", Text = "Processing"},
                        new SelectListItem { Value = "Shipped", Text = "Shipped"}
                    };
            }
        }

    }
}
