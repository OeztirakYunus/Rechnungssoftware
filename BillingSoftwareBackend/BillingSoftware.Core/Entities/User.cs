using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BillingSoftware.Core.Enums;

namespace BillingSoftware.Core.Entities
{
    public class User : EntityObject
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }       
        [Required]
        public Role Role { get; set; } = Role.User;
    }
}
