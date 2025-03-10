﻿using BillingSoftware.Core.Entities;
using ConvertApiDotNet;
using System.IO;
using System.Threading.Tasks;

namespace CommonBase.DocumentCreators
{
    public static class PdfCreator
    {
        private static string SEPARATOR = Globals.SEPARATOR;
        public static async Task<(byte[], string)> CreatePdfForOffer(Offer offer, Company company)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForOffer(offer, company, false);
            var mainPath = Globals.OFFER_PATH + $"{offer.CompanyId}{SEPARATOR}";
            var pdfFile = mainPath + $"Angebot_{offer.OfferNumber}.pdf";
            return await CreatePdf(mainPath, pdfFile, wordFile);
        }

        public static async Task<(byte[], string)> CreatePdfForOrderConfirmation(OrderConfirmation orderConfirmation, Company company)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForOrderConfirmation(orderConfirmation, company, false);
            var mainPath = Globals.OC_PATH + $"{orderConfirmation.CompanyId}{SEPARATOR}";
            var pdfFile = mainPath + $"Auftragsbestätigung_{orderConfirmation.OrderConfirmationNumber}.pdf";
            return await CreatePdf(mainPath, pdfFile, wordFile);
        }

        public static async Task<(byte[], string)> CreatePdfForInvoice(Invoice invoice, Company company)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForInvoice(invoice, company, false);
            var mainPath = Globals.INVOICE_PATH + $"{invoice.CompanyId}{SEPARATOR}";
            var pdfFile = mainPath + $"Rechnung_{invoice.InvoiceNumber}.pdf";
            return await CreatePdf(mainPath, pdfFile, wordFile);
        }

        public static async Task<(byte[], string)> CreatePdfForDeliveryNote(DeliveryNote deliveryNote, Company company)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForDeliveryNote(deliveryNote, company, false);
            var mainPath = Globals.DN_PATH + $"{deliveryNote.CompanyId}{SEPARATOR}";
            var pdfFile = mainPath + $"Lieferschein_{deliveryNote.DeliveryNoteNumber}.pdf";
            return await CreatePdf(mainPath, pdfFile, wordFile);
        }

        private static async Task<(byte[], string)> CreatePdf(string mainPath, string pdfFile, string wordFile)
        {
            if (!Directory.Exists(mainPath))
            {
                Directory.CreateDirectory(mainPath);
            }
            if (File.Exists(pdfFile))
            {
                File.Delete(pdfFile);
            }

            var convertApi = new ConvertApi("7TKzD9IlCa6qGmrY");
            var convert = await convertApi.ConvertAsync("docx", "pdf",
                new ConvertApiFileParam("File", wordFile)
            );
            await convert.SaveFilesAsync(mainPath);

            var bytesOfPdf = await System.IO.File.ReadAllBytesAsync(pdfFile);
            File.Delete(pdfFile);
            File.Delete(wordFile);
            return (bytesOfPdf, pdfFile);
        }
    }
}
