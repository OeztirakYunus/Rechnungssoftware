using BillingSoftware.Core.Entities;
using ConvertApiDotNet;
using System.IO;
using System.Threading.Tasks;

namespace CommonBase
{
    public static class PdfCreator
    {
        private const string SAVE_DIRECTORY = @".\documents\";
        public static async Task<(byte[], string)> CreatePdfForOffer(Offer offer)
        {
            var (bytes, filePath) = await DocxCreator.CreateWordForOffer(offer);
            var path = SAVE_DIRECTORY + @$"offers\{offer.CompanyId}\";
            var pdfPath = path + $"Angebot_{offer.OfferNumber}.pdf";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if(File.Exists(pdfPath))
            {
                File.Delete(pdfPath);
            }

            var convertApi = new ConvertApi("rdH5vHXH2xx8JBbX");
			var convert = await convertApi.ConvertAsync("docx", "pdf",
				new ConvertApiFileParam("File", filePath)
			);
			await convert.SaveFilesAsync(path);
            var bytesOfPdf = await System.IO.File.ReadAllBytesAsync(pdfPath);
            return (bytesOfPdf, pdfPath);
        }

        public static async Task CreatePdfForOrderConfirmation(OrderConfirmation orderConfirmation)
        {
            var (bytes, filePath) = await DocxCreator.CreateWordForOrderConfirmation(orderConfirmation);
            var path = SAVE_DIRECTORY + @$"orderConfirmations\{orderConfirmation.CompanyId}\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(path + $"Auftragsbestätigung_{orderConfirmation.OrderConfirmationNumber}.pdf"))
            {
                File.Delete(path + $"Auftragsbestätigung_{orderConfirmation.OrderConfirmationNumber}.pdf");
            }

            var convertApi = new ConvertApi("rdH5vHXH2xx8JBbX");
            var convert = await convertApi.ConvertAsync("docx", "pdf",
                new ConvertApiFileParam("File", filePath)
            );
            await convert.SaveFilesAsync(path);
        }

        public static async Task CreatePdfForDeliveryNote(DeliveryNote deliveryNote)
        {
            var (bytes, filePath) = await DocxCreator.CreateWordForDeliveryNote(deliveryNote);
            var path = SAVE_DIRECTORY + @$"deliveryNotes\{deliveryNote.CompanyId}\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(path + $"Lieferschein_{deliveryNote.DeliveryNoteNumber}.pdf"))
            {
                File.Delete(path + $"Lieferschein_{deliveryNote.DeliveryNoteNumber}.pdf");
            }

            var convertApi = new ConvertApi("rdH5vHXH2xx8JBbX");
            var convert = await convertApi.ConvertAsync("docx", "pdf",
                new ConvertApiFileParam("File", filePath)
            );
            await convert.SaveFilesAsync(path);
        }

        public static async Task CreatePdfForInvoice(Invoice invoice)
        {
            var (bytes, filePath) = await DocxCreator.CreateWordForInvoice(invoice);
            var path = SAVE_DIRECTORY + @$"invoices\{invoice.CompanyId}\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (File.Exists(path + $"Rechnung_{invoice.InvoiceNumber}.pdf"))
            {
                File.Delete(path + $"Rechnung_{invoice.InvoiceNumber}.pdf");
            }

            var convertApi = new ConvertApi("rdH5vHXH2xx8JBbX");
            var convert = await convertApi.ConvertAsync("docx", "pdf",
                new ConvertApiFileParam("File", filePath)
            );
            await convert.SaveFilesAsync(path);
        }
    }
}
