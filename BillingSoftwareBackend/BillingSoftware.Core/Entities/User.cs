using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BillingSoftware.Core.Enums;
using Microsoft.AspNetCore.Identity;

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
        public virtual Company Company { get; set; }
        public void CopyProperties(User other)
        {
            FirstName = other.FirstName;
            LastName = other.LastName;
            Password = other.Password;
            Email = other.Email;
            Role = other.Role;
            Company.CopyProperties(other.Company);
        }
    }
}
