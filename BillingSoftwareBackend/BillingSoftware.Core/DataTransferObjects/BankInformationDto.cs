using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSoftware.Core.DataTransferObjects
{
    public class BankInformationDto
    {
        public string BankName { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
    }
}
