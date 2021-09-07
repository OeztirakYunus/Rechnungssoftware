using BillingSoftware.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillingSoftware.Core.Entities.Client
{
    public class PersonClient : Client
    {
        public Gender Gender { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
