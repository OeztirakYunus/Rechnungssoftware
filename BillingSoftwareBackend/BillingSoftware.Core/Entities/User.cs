using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BillingSoftware.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace BillingSoftware.Core.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }  
        public virtual Company Company { get; set; }
        public void CopyProperties(User other)
        {
            FirstName = other.FirstName;
            LastName = other.LastName;
            //Company.CopyProperties(other.Company);
        }
    }
}
