using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase
{
    internal static class Globals
    {
        private static bool IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        public static string SEP = IsDevelopment ? @"\" : "/";
        public static string TEMPLATE_DIRECTORY = $@".{SEP}templates{SEP}default{SEP}";
        private static string SAVE_DIRECTORY = $@".{SEP}documents{SEP}";
        public static string OFFER_PATH = SAVE_DIRECTORY + @$"offers{SEP}";
        public static string OC_PATH = SAVE_DIRECTORY + @$"orderConfirmations{SEP}";
        public static string INVOICE_PATH = SAVE_DIRECTORY + @$"invoices{SEP}";
        public static string DN_PATH = SAVE_DIRECTORY + @$"deliveryNotes{SEP}";
    }
}
