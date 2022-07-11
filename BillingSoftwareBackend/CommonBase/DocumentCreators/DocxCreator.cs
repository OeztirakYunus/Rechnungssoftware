using BillingSoftware.Core.Entities;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CommonBase.DocumentCreators
{
    public static class DocxCreator
    {
        private static string TEMPLATE_DIRECTORY = Globals.TEMPLATE_DIRECTORY;
        private static string SEP = Globals.SEPARATOR;

        public async static Task<(byte[], string)> CreateWordForOffer(Offer offer, bool deleteWordAfterReturn = true)
        {
            var templateFile = TEMPLATE_DIRECTORY + "offer_template.docx";
            var path = Globals.OFFER_PATH + $"{offer.CompanyId}{SEP}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = path + $"Angebot_{ offer.OfferNumber}.docx";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            System.IO.File.Copy(templateFile, filePath);

            CreateBasics(filePath, offer.Company, offer.DocumentInformation);
            CreateBodyForOffer(filePath, offer);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            if (deleteWordAfterReturn)
            {
                File.Delete(filePath);
            }
            return (bytes, filePath);
        }

        public async static Task<(byte[], string)> CreateWordForOrderConfirmation(OrderConfirmation orderConfirmation, bool deleteWordAfterReturn = true)
        {
            var templateFile = TEMPLATE_DIRECTORY + "order_confirmation_template.docx";
            var path = Globals.OC_PATH + $"{orderConfirmation.CompanyId}{SEP}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = path + $"Auftragsbestätigung_{ orderConfirmation.OrderConfirmationNumber}.docx";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            System.IO.File.Copy(templateFile, filePath);

            CreateBasics(filePath, orderConfirmation.Company, orderConfirmation.DocumentInformation);
            CreateBodyForOrderConfirmation(filePath, orderConfirmation);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            if (deleteWordAfterReturn)
            {
                File.Delete(filePath);
            }
            return (bytes, filePath);
        }

        public async static Task<(byte[], string)> CreateWordForInvoice(Invoice invoice, bool deleteWordAfterReturn = true)
        {
            var templateFile = TEMPLATE_DIRECTORY + "invoice_template.docx";
            var path = Globals.INVOICE_PATH + $"{invoice.CompanyId}{SEP}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = path + $"Rechnung_{ invoice.InvoiceNumber}.docx";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            System.IO.File.Copy(templateFile, filePath);

            CreateBasics(filePath, invoice.Company, invoice.DocumentInformation);
            CreateBodyForInvoice(filePath, invoice);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            if (deleteWordAfterReturn)
            {
                File.Delete(filePath);
            }
            return (bytes, filePath);
        }

        public async static Task<(byte[], string)> CreateWordForDeliveryNote(DeliveryNote deliveryNote, bool deleteWordAfterReturn = true)
        {
            var templateFile = TEMPLATE_DIRECTORY + "delivery_note_template.docx";
            var path = Globals.DN_PATH + $"{deliveryNote.CompanyId}{SEP}";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = path + $"Lieferschein_{ deliveryNote.DeliveryNoteNumber}.docx";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            System.IO.File.Copy(templateFile, filePath);

            CreateBasics(filePath, deliveryNote.Company, deliveryNote.DocumentInformations);
            CreateBodyForDeliveryNote(filePath, deliveryNote);
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            if (deleteWordAfterReturn)
            {
                File.Delete(filePath);
            }
            return (bytes, filePath);
        }

        private static void CreateBasics(string path,Company company, DocumentInformations documentInformations)
        {
            CreateFooter(path, company);
            CreateHeader(path, company);
            CreateBodyWithUniversalInformations(path, company, documentInformations);
            CreateTable(path, documentInformations);
        }

        //Footer
        private static void CreateFooter(string path, Company company)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.FooterParts.First().GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("company.name", company.CompanyName);
                docText = docText.Replace("company.street", company.Address.Street);
                docText = docText.Replace("company.postCode", company.Address.ZipCode);
                docText = docText.Replace("company.city", company.Address.City);
                docText = docText.Replace("company.ustNumber", company.UstNumber);
                docText = docText.Replace("company.phoneNumber", company.PhoneNumber);
                docText = docText.Replace("company.email", company.Email);
                docText = docText.Replace("company.iban", company.BankInformation.Iban);
                docText = docText.Replace("company.bankName", company.BankInformation.BankName);
                docText = docText.Replace("company.bic", company.BankInformation.Bic);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.FooterParts.First().GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }
        }

        //Body with universal informations
        private static void CreateBodyWithUniversalInformations(string path, Company company, DocumentInformations documentInformation)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("company.name", company.CompanyName);
                docText = docText.Replace("company.street", company.Address.Street);
                docText = docText.Replace("company.postCode", company.Address.ZipCode);
                docText = docText.Replace("company.city", company.Address.City);
                docText = docText.Replace("client.street", documentInformation.Client.Address.Street);
                docText = docText.Replace("client.postCode", documentInformation.Client.Address.ZipCode);
                docText = docText.Replace("client.city", documentInformation.Client.Address.City);
                docText = docText.Replace("client.gender", documentInformation.Client.Gender == BillingSoftware.Core.Enums.Gender.Male ? "Herr" : "Frau");
                docText = docText.Replace("contact.name", documentInformation.ContactPerson.LastName + " " + documentInformation.ContactPerson.FirstName);

                var nameOfOrganisation = "";
                if (!string.IsNullOrEmpty(documentInformation.Client.NameOfOrganisation))
                {
                    nameOfOrganisation = documentInformation.Client.NameOfOrganisation;
                }
                docText = docText.Replace("client.nameOfOrganisation", nameOfOrganisation);

                var clientName = "";
                var greeting = "geehrte Damen und Herren,";
                if (!string.IsNullOrEmpty(documentInformation.Client.FirstName) && !string.IsNullOrEmpty(documentInformation.Client.LastName))
                {
                    clientName = documentInformation.Client.LastName + " " + documentInformation.Client.FirstName;
                    if (documentInformation.Client.Gender == BillingSoftware.Core.Enums.Gender.Male)
                    {
                        greeting = "geehrter Herr " + clientName + ",";
                    }
                    else if (documentInformation.Client.Gender == BillingSoftware.Core.Enums.Gender.Female)
                    {
                        greeting = "geehrte Frau " + clientName + ",";
                    }
                }
                docText = docText.Replace("client.name", clientName);
                docText = docText.Replace("client.greeting", greeting);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }
        }

        //Body For Offer
        private static void CreateBodyForOffer(string path, Offer offer)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("offer.date", offer.OfferDate.ToString("dd.MM.yyyy"));
                docText = docText.Replace("offer.validUntil", offer.ValidUntil.ToString("dd.MM.yyyy"));
                docText = docText.Replace("offer.subject", offer.Subject);
                docText = docText.Replace("offer.flowText", offer.FlowText);
                docText = docText.Replace("offer.headerText", offer.HeaderText);
                
                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }
        }

        //Body For OrderConfirmation
        private static void CreateBodyForOrderConfirmation(string path, OrderConfirmation orderConfirmation)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("oc.date", orderConfirmation.OrderConfirmationDate.ToString("dd.MM.yyyy"));
                docText = docText.Replace("oc.subject", orderConfirmation.Subject);
                docText = docText.Replace("oc.flowText", orderConfirmation.FlowText);
                docText = docText.Replace("oc.headerText", orderConfirmation.HeaderText);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }
        }

        //Body For Invoice
        private static void CreateBodyForInvoice(string path, Invoice invoice)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("invoice.date", invoice.InvoiceDate.ToString("dd.MM.yyyy"));
                docText = docText.Replace("invoice.paymentTerm", invoice.PaymentTerm.ToString("dd.MM.yyyy"));
                docText = docText.Replace("invoice.subject", invoice.Subject);
                docText = docText.Replace("invoice.flowText", invoice.FlowText);
                docText = docText.Replace("invoice.headerText", invoice.HeaderText);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }
        }

        //Body For DeliveryNote
        private static void CreateBodyForDeliveryNote(string path, DeliveryNote deliveryNote)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("dn.date", deliveryNote.DeliveryNoteDate.ToString("dd.MM.yyyy"));
                docText = docText.Replace("dn.subject", deliveryNote.Subject);
                docText = docText.Replace("dn.flowText", deliveryNote.FlowText);
                docText = docText.Replace("dn.headerText", deliveryNote.HeaderText);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }
        }

        //Header
        private static void CreateHeader(string path, Company company)
        {
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(path, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.HeaderParts.First().GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("company.name", company.CompanyName);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.HeaderParts.First().GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }
        }

        // Insert a table into a word processing document.
        private static void CreateTable(string fileName, DocumentInformations documentInformation)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(fileName, true))
            {
                Table tbl = new Table();
                TableProperties props = new TableProperties();
                tbl.AppendChild<TableProperties>(props);

                TableLayout tl = new TableLayout() { Type = TableLayoutValues.Fixed };
                props.TableLayout = tl;

                Bold bold1 = new Bold();
                bold1.Val = OnOffValue.FromBoolean(true);
                Run headerRun1 = new Run();
                RunProperties headerRunProperties1 = headerRun1.AppendChild(new RunProperties());
                headerRunProperties1.AppendChild(bold1);
                headerRun1.Append(new Text("Position"));

                Bold bold2 = new Bold();
                bold2.Val = OnOffValue.FromBoolean(true);
                Run headerRun2 = new Run();
                RunProperties headerRunProperties2 = headerRun2.AppendChild(new RunProperties());
                headerRunProperties2.AppendChild(bold2);
                headerRun2.Append(new Text("Bezeichnung"));

                Bold bold3 = new Bold();
                bold3.Val = OnOffValue.FromBoolean(true);
                Run headerRun3 = new Run();
                RunProperties headerRunProperties3 = headerRun3.AppendChild(new RunProperties());
                headerRunProperties3.AppendChild(bold3);
                headerRun3.Append(new Text("Menge"));

                Bold bold4 = new Bold();
                bold4.Val = OnOffValue.FromBoolean(true);
                Run headerRun4 = new Run();
                RunProperties headerRunProperties4 = headerRun4.AppendChild(new RunProperties());
                headerRunProperties4.AppendChild(bold4);
                headerRun4.Append(new Text("Einheit"));

                Bold bold5 = new Bold();
                bold5.Val = OnOffValue.FromBoolean(true);
                Run headerRun5 = new Run();
                RunProperties headerRunProperties5 = headerRun5.AppendChild(new RunProperties());
                headerRunProperties5.AppendChild(bold5);
                headerRun5.Append(new Text("Einzel (€)"));

                Bold bold6 = new Bold();
                bold6.Val = OnOffValue.FromBoolean(true);
                Run headerRun6 = new Run();
                RunProperties headerRunProperties6 = headerRun6.AppendChild(new RunProperties());
                headerRunProperties6.AppendChild(bold6);
                headerRun6.Append(new Text("Gesamt (€)"));

                //Header
                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell(new Paragraph(headerRun1));
                TableCell tc2 = new TableCell(new Paragraph(headerRun2));
                TableCell tc3 = new TableCell(new Paragraph(headerRun3));
                TableCell tc4 = new TableCell(new Paragraph(headerRun4));
                TableCell tc5 = new TableCell(new Paragraph(headerRun5));
                TableCell tc6 = new TableCell(new Paragraph(headerRun6));
                tc1.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1000" }));
                tc2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "4000" }));
                tc3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "900" }));
                tc4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));
                tc5.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1100" }));
                tc6.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1300" }));

                tr.Append(tc1, tc2, tc3, tc4, tc5, tc6);
                tbl.AppendChild(tr);

                //Positions
                var posCounter = 1;
                foreach (var item in documentInformation.Positions)
                {
                    TableRow tr1 = new TableRow();
                    TableCell tcData1 = new TableCell(new Paragraph(new Run(new Text(posCounter.ToString()))));

                    Run articleDescRun = new Run();
                    articleDescRun.Append(new Text(item.Product.ArticleNumber + " " + item.Product.ProductName));

                    if(item.Discount > 0)
                    {
                        var discountSymbol = "%";
                        switch (item.TypeOfDiscount)
                        {
                            case BillingSoftware.Core.Enums.TypeOfDiscount.Percent:
                                discountSymbol = "%";
                                break;
                            case BillingSoftware.Core.Enums.TypeOfDiscount.Euro:
                                discountSymbol = "€";
                                break;
                            default:
                                break;
                        }
                        articleDescRun.Append(new Break(),new Text("Rabatt: " + item.Discount + " " + discountSymbol));
                    }

                    TableCell tcData2 = new TableCell(new Paragraph(articleDescRun));
                    TableCell tcData3 = new TableCell(new Paragraph(new Run(new Text(item.Quantity.ToString()))));
                    TableCell tcData4 = new TableCell(new Paragraph(new Run(new Text(item.Product.Unit.ToString()))));
                    TableCell tcData5 = new TableCell(new Paragraph(new Run(new Text(item.Product.SellingPriceNet.ToString()))));
                    TableCell tcData6 = new TableCell(new Paragraph(new Run(new Text(item.TotalPriceNet.ToString()))));
                    tcData1.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1000" }));
                    tcData2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "4000" }));
                    tcData3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "900" }));
                    tcData4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));
                    tcData5.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1100" }));
                    tcData6.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1300" }));
                    tr1.Append(tcData1, tcData2, tcData3, tcData4, tcData5, tcData6);
                    tbl.AppendChild(tr1);
                    posCounter++;
                }

                Bold bold = new Bold();
                bold.Val = OnOffValue.FromBoolean(true);
                Run run = new Run();
                RunProperties runProperties = run.AppendChild(new RunProperties());
                runProperties.AppendChild(bold);
                run.Append(new Text($"Summe Netto"), new Break(), new Text($"USt {documentInformation.Tax}%"), new Break(), new Text("Gesamt"));

                TableRow tr3 = new TableRow();
                TableCell tcSumNet1 = new TableCell(new Paragraph(new Run(new Text(""))));
                TableCell tcSumNet2 = new TableCell(new Paragraph(new Run(new Text(""))));
                TableCell tcSumNet3 = new TableCell(new Paragraph(new Run(new Text(""))));
                TableCell tcSumNet4 = new TableCell(new Paragraph(run));
                TableCell tcSumNet5 = new TableCell(new Paragraph(new Run(new Text(""))));
                TableCell tcSumNet6 = new TableCell(new Paragraph(new Run(new Text($"€ {documentInformation.TotalPriceNet}"), new Break(), new Text($"€ {documentInformation.TotalPriceNet * (documentInformation.Tax / 100)}"), new Break(), new Text($"€ {documentInformation.TotalPriceGross}"))));
                tcSumNet1.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1000" }));
                tcSumNet2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "4000" }));
                tcSumNet3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "900" }));
                tcSumNet4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));
                tcSumNet5.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1100" }));
                tcSumNet6.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1300" }));
                tr3.Append(tcSumNet1, tcSumNet2, tcSumNet3, tcSumNet4, tcSumNet5, tcSumNet6);
                tbl.AppendChild(tr3);

                string tablePlaceholder = "{{positions}}";
                Text tablePl = doc.MainDocumentPart.Document
                    .Descendants<Text>()
                    .Where(x => x.Text.Contains(tablePlaceholder))
                    .FirstOrDefault();
                if (tablePl != null)
                {
                    var parent = tablePl.Parent.Parent.Parent;
                    parent.InsertAfter(tbl, tablePl.Parent.Parent);
                    tablePl.Text = tablePl.Text.Replace(tablePlaceholder, "");
                    doc.MainDocumentPart.Document.Save();
                }
            }
        }
    }
}
