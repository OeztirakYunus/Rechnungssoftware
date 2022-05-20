using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.Entities
{
    public class BankInformation : EntityObject
    {
        [Required]
        public string BankName { get; set; }
        [Required]
        public string Iban { get; set; }
        [Required]
        public string Bic { get; set; }
    }
}
