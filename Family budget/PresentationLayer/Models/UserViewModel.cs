using Family_budget.DataAccessLayer.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Family_budget.PresentationLayer.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not the same")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime PasswordDate { get; set; }

        public string PreviousPasswords { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Length should be 10 symbols")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid phone number. Length must contain 10 symbols")]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public UserRole Role { get; set; }
    }
}
