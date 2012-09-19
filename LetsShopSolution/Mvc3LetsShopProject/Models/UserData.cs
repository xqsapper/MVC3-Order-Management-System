using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace Mvc3LetsShopProject.Models
{
    public class UserData
    {
        [Key]
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public long CardNumber { get; set; }
        public string CardType { get; set; }
        public DateTime CardExpiryDate { get; set; }
        public long PhoneNumber{ get; set; }
        public string EmailId { get; set; }
    }

    public class UserCredentials
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public class UserSignUp
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
    }

    public class UserDBContext : DbContext
    {
        public DbSet<UserData> Users { get; set; }
    }

}