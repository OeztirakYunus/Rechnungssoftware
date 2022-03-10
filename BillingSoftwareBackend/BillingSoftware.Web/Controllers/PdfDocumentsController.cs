//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using BillingSoftware.Core.Contracts;
//using BillingSoftware.Core.Entities;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace BillingSoftware.Web.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PdfDocumentsController : ControllerBase
//    {
//        private const string PATH = @".\pdfs\";
//        [HttpGet]
//        public ActionResult<IEnumerable<string>> GetPdfDocuments()
//        {
//            try
//            {
//                if (!Directory.Exists(PATH))
//                {
//                    return BadRequest("No files available.");
//                }

//                var pdfDocumentsTemp = Directory.GetFiles(PATH);
//                var pdfDocumentNames = new List<string>();
//                foreach (var pdfDocumentName in pdfDocumentsTemp)
//                {
//                    pdfDocumentNames.Add(pdfDocumentName.Split(@"\").Last());
//                }

//                return Ok(pdfDocumentNames);
//            }
//            catch (System.Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpGet("{fileName}")]
//        public async Task<IActionResult> GetPdfDocument(string fileName)
//        {
//            try
//            {
//                string filePath = Path.Combine(PATH, fileName);
//                if (!System.IO.File.Exists(filePath))
//                {
//                    return BadRequest($"File \"{fileName}\" not found!");
//                }

//                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
//                return File(bytes, "application/pdf", Path.GetFileName(filePath));
//            }
//            catch (System.Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> PostPdfDocument(IFormFile file)
//        {
//            try
//            {
//                if (file.Length > 0)
//                {
//                    var filePath = Path.Combine(PATH, file.FileName);
//                    if (!System.IO.File.Exists(PATH))
//                    {
//                        Directory.CreateDirectory(PATH);
//                    }

//                    if (System.IO.File.Exists(filePath))
//                    {
//                        return BadRequest($"File \"{file.FileName}\" exists already!");
//                    }

//                    using (var stream = System.IO.File.Create(filePath))
//                    {
//                        await file.CopyToAsync(stream);
//                    }
//                }

//                return Ok($"File \"{file.FileName}\" saved.");
//            }
//            catch (System.Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }

//        [HttpDelete("{fileName}")]
//        public IActionResult DeletePdfDocument(string fileName)
//        {
//            try
//            {
//                var filePath = Path.Combine(PATH,fileName);
//                if (!System.IO.File.Exists(filePath))
//                {
//                    return BadRequest($"File \"{fileName}\" not found!");
//                }

//                System.IO.File.Delete(filePath);
//                return Ok($"File \"{fileName}\" deleted.");
//            }
//            catch (System.Exception ex)
//            {
//                return BadRequest(ex.Message);
//            }
//        }
//    }
//}
