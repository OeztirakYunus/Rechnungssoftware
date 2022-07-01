﻿using BillingSoftware.Core.Entities;
using ConvertApiDotNet;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Office.Interop.Word;
using OpenXmlPowerTools;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonBase.DocumentCreators
{
    public static class PdfCreator
    {
        private static string SEPARATOR = Globals.SEPARATOR;
        public static async Task<(byte[], string)> CreatePdfForOffer(Offer offer)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForOffer(offer, false);
            var mainPath = Globals.OFFER_PATH + $"{offer.CompanyId}{SEPARATOR}";
            var pdfFile = mainPath + $"Angebot_{offer.OfferNumber}.pdf";
            return await CreatePdf(mainPath, pdfFile, wordFile);
        }

        public static async Task<(byte[], string)> CreatePdfForOrderConfirmation(OrderConfirmation orderConfirmation)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForOrderConfirmation(orderConfirmation, false);
            var mainPath = Globals.OC_PATH + $"{orderConfirmation.CompanyId}{SEPARATOR}";
            var pdfFile = mainPath + $"Auftragsbestätigung_{orderConfirmation.OrderConfirmationNumber}.pdf";
            return await CreatePdf(mainPath, pdfFile, wordFile);
        }

        public static async Task<(byte[], string)> CreatePdfForInvoice(Invoice invoice)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForInvoice(invoice, false);
            var mainPath = Globals.INVOICE_PATH + $"{invoice.CompanyId}{SEPARATOR}";
            var pdfFile = mainPath + $"Rechnung_{invoice.InvoiceNumber}.pdf";
            return await CreatePdf(mainPath, pdfFile, wordFile);
        }

        public static async Task<(byte[], string)> CreatePdfForDeliveryNote(DeliveryNote deliveryNote)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForDeliveryNote(deliveryNote, false);
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

            //var convertApi = new ConvertApi("rdH5vHXH2xx8JBbX");
            //var convert = await convertApi.ConvertAsync("docx", "pdf",
            //    new ConvertApiFileParam("File", wordFile)
            //);
            //await convert.SaveFilesAsync(mainPath);

            await System.Threading.Tasks.Task.Run(() =>
            {
                Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document wordDocument = appWord.Documents.Open(wordFile);
                wordDocument.ExportAsFixedFormat(pdfFile, WdExportFormat.wdExportFormatPDF);
                wordDocument.Close();
                appWord.Quit();
            });


            //byte[] byteArray = File.ReadAllBytes(wordFile);
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    memoryStream.Write(byteArray, 0, byteArray.Length);
            //    using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
            //    {
            //        HtmlConverterSettings settings = new HtmlConverterSettings()
            //        {
            //            PageTitle = "My Page Title"
            //        };
            //        XElement html = HtmlConverter.ConvertToHtml(doc, settings);

            //        File.WriteAllText(pdfFile, html.ToStringNewLineOnAttributes());
            //    }
            //}


            var bytesOfPdf = await System.IO.File.ReadAllBytesAsync(pdfFile);
            File.Delete(pdfFile);
            File.Delete(wordFile);
            return (bytesOfPdf, pdfFile);
        }
    }
}
