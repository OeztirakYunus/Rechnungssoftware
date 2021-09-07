using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BillingSoftware.Core.Enums;

namespace BillingSoftware.Core.Entities
{
    public class User : EntityObject
    {
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }        
        public Role Role { get; set; } = Role.USER;
    }
}
