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
        public Guid CompanyId { get; set; }

        //Navigation Properties
        public virtual Company Company { get; set; }
    }
}
