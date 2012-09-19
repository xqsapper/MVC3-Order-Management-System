using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace DataClassLibrary.Models
{
    public class UserData
    {
        [Key]
        public string CustomerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        [Required(ErrorMessage = "Security information is required to protect your account. Please enter.")]
        public string SecurityQuestion { get; set; }

        [Required(ErrorMessage = "Security information is required to protect your account. Please enter.")]
        public string SecurityAnswer { get; set; }

        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }

        [RegularExpression("[0-9]{16}", ErrorMessage = "Card Number must contain 16 digits.")]
        public long CardNumber { get; set; }

        public string CardType { get; set; }

        public DateTime CardExpiryDate { get; set; }

        [RegularExpression("[0-9]{10}", ErrorMessage = "Phone Number must contain 10 digits.")]
        public long PhoneNumber{ get; set; }

        [RegularExpression("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Invalid. Format must be: someone@example.com")]
        public string EmailId { get; set; }
    }

    //______________________________________________________________________________________

    public class UserCredentials
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    //______________________________________________________________________________________

    public class UserSignUp
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
    }

    //______________________________________________________________________________________

    public class UserDBContext : DbContext
    {
        public DbSet<UserData> Users { get; set; }
    }

    //______________________________________________________________________________________

}
