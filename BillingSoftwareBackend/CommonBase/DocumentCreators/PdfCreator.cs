using BillingSoftware.Core.Entities;
using ConvertApiDotNet;
using System.IO;
using System.Threading.Tasks;

namespace CommonBase.DocumentCreators
{
    public static class PdfCreator
    {
        private const string SAVE_DIRECTORY = @".\documents\";
        public static async Task<(byte[], string)> CreatePdfForOffer(Offer offer)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForOffer(offer, false);
            var mainPath = SAVE_DIRECTORY + @$"offers\{offer.CompanyId}\";
            var pdfFile = mainPath + $"Angebot_{offer.OfferNumber}.pdf";
            return await CreatePdf(mainPath, pdfFile, wordFile);
        }

        public static async Task<(byte[], string)> CreatePdfForOrderConfirmation(OrderConfirmation orderConfirmation)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForOrderConfirmation(orderConfirmation, false);
            var mainPath = SAVE_DIRECTORY + @$"orderConfirmations\{orderConfirmation.CompanyId}\";
            var pdfFile = mainPath + $"Auftragsbestätigung_{orderConfirmation.OrderConfirmationNumber}.pdf";
            return await CreatePdf(mainPath, pdfFile, wordFile);
        }

        public static async Task<(byte[], string)> CreatePdfForInvoice(Invoice invoice)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForInvoice(invoice, false);
            var mainPath = SAVE_DIRECTORY + @$"invoices\{invoice.CompanyId}\";
            var pdfFile = mainPath + $"Rechnung_{invoice.InvoiceNumber}.pdf";
            return await CreatePdf(mainPath, pdfFile, wordFile);
        }

        public static async Task<(byte[], string)> CreatePdfForDeliveryNote(DeliveryNote deliveryNote)
        {
            var (_, wordFile) = await DocxCreator.CreateWordForDeliveryNote(deliveryNote, false);
            var mainPath = SAVE_DIRECTORY + @$"deliveryNotes\{deliveryNote.CompanyId}\";
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

            var convertApi = new ConvertApi("rdH5vHXH2xx8JBbX");
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
