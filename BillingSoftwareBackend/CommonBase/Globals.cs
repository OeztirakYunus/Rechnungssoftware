using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBase
{
    internal static class Globals
    {
        private static bool IsDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
        public static string SEPARATOR = IsDevelopment ? @"\" : "/";
        public static string TEMPLATE_DIRECTORY = $@"{Directory.GetCurrentDirectory()}{SEPARATOR}templates{SEPARATOR}default{SEPARATOR}";
        private static string SAVE_DIRECTORY = $@"{Directory.GetCurrentDirectory()}{SEPARATOR}documents{SEPARATOR}";
        public static string OFFER_PATH = SAVE_DIRECTORY + @$"offers{SEPARATOR}";
        public static string OC_PATH = SAVE_DIRECTORY + @$"orderConfirmations{SEPARATOR}";
        public static string INVOICE_PATH = SAVE_DIRECTORY + @$"invoices{SEPARATOR}";
        public static string DN_PATH = SAVE_DIRECTORY + @$"deliveryNotes{SEPARATOR}";
    }
}
