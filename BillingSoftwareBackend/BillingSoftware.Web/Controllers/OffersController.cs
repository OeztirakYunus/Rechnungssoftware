using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using BillingSoftware.Persistence;
using CommonBase.Extensions;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OffersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private const string TEMPLATE = @".\templates\default\offer_template.docx";
        private const string SAVE_DIRECTORY = @".\documents\offers";


        public OffersController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offer>>> GetOffers()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var offers = await _uow.OfferRepository.GetAllAsync();
                offers = offers.Where(i => user.Company.Offers.Any(a => a.Id.Equals(i.Id))).ToArray();

                return Ok(offers);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetOffer(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this offer!" });
                }

                var offer = await _uow.OfferRepository.GetByIdAsync(guid);
                return offer;
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutOffer(Offer offer)
        {
            try
            {
                if (!await CheckAuthorization(offer.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to update this offer!" });
                }

                var entity = await _uow.OfferRepository.GetByIdAsync(offer.Id);
                offer.CopyProperties(entity);
                await _uow.OfferRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostOffer(Offer offer)
        {
            try
            {
                if (!await CheckAuthorization(offer.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this offer!" });
                }

                await _uow.OfferRepository.AddAsync(offer);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this offer!" });
                }

                await _uow.OfferRepository.Remove(guid);
                await _uow.SaveChangesAsync();
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("offer-to-order-confirmation/{offerId}")]
        public async Task<IActionResult> OfferToOrderConfirmation(string offerId)
        {
            try
            {
                var guid = Guid.Parse(offerId);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to transform this offer!" });
                }

                var offer = await _uow.OfferRepository.GetByIdAsync(guid);
                var orderConfirmation = await _uow.OfferRepository.OfferToOrderConfirmation(offer);
                await _uow.SaveChangesAsync();
                return Ok(orderConfirmation);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + "\n" + ex.InnerException.Message);
            }
        }

        [HttpGet("get-as-word/{offerId}")]
        public async Task/*<IActionResult>*/ GetOfferAsWord(string offerId)
        {
            //var guid = Guid.Parse(offerId);
            //if (!await CheckAuthorization(guid))
            //{
            //    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this offer as word!" });
            //}
            await GetAsWord(offerId);
            
        }

        private async Task GetAsWord(string offerId)
        {
            var guid = Guid.Parse(offerId);
            var offer = await _uow.OfferRepository.GetByIdAsync(guid);
            var newFile = SAVE_DIRECTORY + @$"\Angebot_{offer.OfferNumber}.docx";
            System.IO.File.Copy(TEMPLATE, newFile);

            //Footer
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(newFile, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.FooterParts.First().GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("company.name", offer.Company.CompanyName);
                docText = docText.Replace("company.street", offer.Company.Addresses[0].Street);
                docText = docText.Replace("company.postCode", offer.Company.Addresses[0].ZipCode);
                docText = docText.Replace("company.city", offer.Company.Addresses[0].City);
                docText = docText.Replace("company.ustNumber", offer.Company.UstNumber);
                docText = docText.Replace("company.phoneNumber", offer.Company.PhoneNumber);
                docText = docText.Replace("company.email", offer.Company.Email);
                docText = docText.Replace("company.iban", offer.Company.Iban);
                docText = docText.Replace("company.bankName", offer.Company.BankName);
                docText = docText.Replace("company.bic", offer.Company.Bic);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.FooterParts.First().GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }

            //Header
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(newFile, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.HeaderParts.First().GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("company.name", offer.Company.CompanyName);

                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.HeaderParts.First().GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                wordDoc.Save();
            }

            //Body
            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(newFile, true))
            {
                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = docText.Replace("company.name", offer.Company.CompanyName);
                docText = docText.Replace("company.street", offer.Company.Addresses[0].Street);
                docText = docText.Replace("company.postCode", offer.Company.Addresses[0].ZipCode);
                docText = docText.Replace("company.city", offer.Company.Addresses[0].City);
                docText = docText.Replace("client.street", offer.DocumentInformation.Client.Addresses[0].Street);
                docText = docText.Replace("client.postCode", offer.DocumentInformation.Client.Addresses[0].ZipCode);
                docText = docText.Replace("client.city", offer.DocumentInformation.Client.Addresses[0].City);
                docText = docText.Replace("client.gender", offer.DocumentInformation.Client.Gender == Core.Enums.Gender.Male ? "Herr" : "Frau");
                docText = docText.Replace("offer.date", offer.OfferDate.ToString("dd.MM.yyyy"));
                docText = docText.Replace("offer.validUntil", offer.ValidUntil.ToString("dd.MM.yyyy"));
                docText = docText.Replace("offer.subject", offer.Subject);
                docText = docText.Replace("offer.flowText", offer.FlowText);
                docText = docText.Replace("offer.headerText", offer.HeaderText);
                docText = docText.Replace("contact.name", offer.DocumentInformation.ContactPerson.LastName + " " + offer.DocumentInformation.ContactPerson.FirstName);
                docText = docText.Replace("offer.priceNet", offer.DocumentInformation.TotalPriceNet.ToString());
                docText = docText.Replace("offer.ustPrice", (offer.DocumentInformation.TotalPriceNet * 0.2).ToString());
                docText = docText.Replace("offer.total", offer.DocumentInformation.TotalPriceGross.ToString());

                var nameOfOrganisation = "";
                if (!string.IsNullOrEmpty(offer.DocumentInformation.Client.NameOfOrganisation))
                {
                    nameOfOrganisation = offer.DocumentInformation.Client.NameOfOrganisation;
                }
                docText = docText.Replace("client.nameOfOrganisation", nameOfOrganisation);

                var clientName = "";
                var greeting = "geehrte Damen und Herren,";
                if (!string.IsNullOrEmpty(offer.DocumentInformation.Client.FirstName) && !string.IsNullOrEmpty(offer.DocumentInformation.Client.LastName))
                {
                    clientName = offer.DocumentInformation.Client.LastName + " " + offer.DocumentInformation.Client.FirstName;
                    if (offer.DocumentInformation.Client.Gender == Core.Enums.Gender.Male)
                    {
                        greeting = "geehrter Herr " + clientName + ",";
                    }
                    else if (offer.DocumentInformation.Client.Gender == Core.Enums.Gender.Female)
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

            //table
            CreateTable(newFile, offer.DocumentInformation);
        }

        // Insert a table into a word processing document.
        private void CreateTable(string fileName, DocumentInformations documentInformation)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(fileName, true))
            {
                Table tbl = new Table();
                TableProperties props = new TableProperties();
                tbl.AppendChild<TableProperties>(props);

                // Now we create a new layout and make it "fixed".
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
                tc6.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1200" }));

                tr.Append(tc1, tc2, tc3, tc4, tc5, tc6);
                tbl.AppendChild(tr);

                var posCounter = 1;
                foreach (var item in documentInformation.Positions)
                {
                    TableRow tr1 = new TableRow();
                    TableCell tcData1 = new TableCell(new Paragraph(new Run(new Text(posCounter.ToString()))));
                    TableCell tcData2 = new TableCell(new Paragraph(new Run(new Text(item.Product.ArticleNumber + " " + item.Product.ProductName))));
                    TableCell tcData3 = new TableCell(new Paragraph(new Run(new Text(item.Quantity.ToString()))));
                    TableCell tcData4 = new TableCell(new Paragraph(new Run(new Text(item.Product.Unit.ToString()))));
                    TableCell tcData5 = new TableCell(new Paragraph(new Run(new Text(item.Product.SellingPriceNet.ToString()))));
                    TableCell tcData6 = new TableCell(new Paragraph(new Run(new Text(item.TotalPriceNet.ToString()))));
                    tcData1.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1000" }));
                    tcData2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "4000" }));
                    tcData3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "900" }));
                    tcData4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));
                    tcData5.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1100" }));
                    tcData6.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1200" }));
                    tr1.Append(tcData1, tcData2, tcData3, tcData4, tcData5, tcData6);
                    tbl.AppendChild(tr1);
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
                TableCell tcSumNet6 = new TableCell(new Paragraph(new Run(new Text($"€ {documentInformation.TotalPriceNet}") , new Break(), new Text($"€ {documentInformation.TotalPriceNet * (documentInformation.Tax / 100)}"), new Break(), new Text($"€ {documentInformation.TotalPriceGross}"))));
                tcSumNet1.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1000" }));
                tcSumNet2.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "4000" }));
                tcSumNet3.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "900" }));
                tcSumNet4.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1500" }));
                tcSumNet5.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1100" }));
                tcSumNet6.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "1000" }));
                tr3.Append(tcSumNet1, tcSumNet2, tcSumNet3, tcSumNet4, tcSumNet5, tcSumNet6);
                tbl.AppendChild(tr3);

                string tablePlaceholder = "{{offerPositions}}";
                Text tablePl = doc.MainDocumentPart.Document
                    .Descendants<Text>()
                    .Where(x => x.Text.Contains(tablePlaceholder))
                    .FirstOrDefault();
                if (tablePl != null)
                {
                    //Insert the table after the paragraph.
                    var parent = tablePl.Parent.Parent.Parent;
                    parent.InsertAfter(tbl, tablePl.Parent.Parent);
                    tablePl.Text = tablePl.Text.Replace(tablePlaceholder, "");
                    doc.MainDocumentPart.Document.Save();
                }

               // mainDocumentPart.Document.Save();

            }
        }

        private async Task<bool> CheckAuthorization(Guid offerId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.Company.Offers.Any(i => i.Id.Equals(offerId));
        }
    }
}
