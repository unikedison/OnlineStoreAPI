using System;

namespace OnlineStore_API.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string VerificationCode { get; set; }
        public string CodeValidity { get; set; }
        public DateTime DOB { get; set; } = DateTime.MinValue;
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Role { get; set; }

    }
}
