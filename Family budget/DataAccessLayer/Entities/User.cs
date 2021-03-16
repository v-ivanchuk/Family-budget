using Family_budget.DataAccessLayer.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Family_budget.DataAccessLayer
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime PasswordDate { get; set; }

        public string PreviousPasswords { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public UserRole Role { get; set; } = UserRole.NotAssigned;
    }
}
