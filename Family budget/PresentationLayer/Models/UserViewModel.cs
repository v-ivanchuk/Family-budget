using Family_budget.DataAccessLayer.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Family_budget.PresentationLayer.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

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
        [Compare("Password", ErrorMessage = "The password is not the same")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Email { get; set; }
        public DateTime PasswordDate { get; set; }
        public string PreviousPasswords { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public UserRole Role { get; set; }
    }
}
