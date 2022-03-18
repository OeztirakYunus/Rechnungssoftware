using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PdfDocumentsController : ControllerBase
    {
        private const string MAIN_DIRECTORY = @".\pdfs\";
        private readonly IUnitOfWork _uow;

        public PdfDocumentsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> GetPdfDocuments()
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("No Company found!");
                }

                var path = MAIN_DIRECTORY + companyId;
                if (!Directory.Exists(path))
                {
                    return BadRequest("No files available.");
                }

                var pdfDocumentsTemp = Directory.GetFiles(path);
                var pdfDocumentNames = new List<string>();
                foreach (var pdfDocumentName in pdfDocumentsTemp)
                {
                    pdfDocumentNames.Add(pdfDocumentName.Split(@"\").Last());
                }

                return Ok(pdfDocumentNames);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetPdfDocument(string fileName)
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("No Company found!");
                }
                var path = MAIN_DIRECTORY + companyId;

                string filePath = Path.Combine(path, fileName);
                if (!System.IO.File.Exists(filePath))
                {
                    return BadRequest($"File \"{fileName}\" not found!");
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                return File(bytes, "application/pdf", Path.GetFileName(filePath));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostPdfDocument(IFormFile file)
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("No Company found!");
                }
                var path = MAIN_DIRECTORY + companyId;

                if (file.Length > 0)
                {
                    var filePath = Path.Combine(path, file.FileName);
                    if (!System.IO.File.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    if (System.IO.File.Exists(filePath))
                    {
                        return BadRequest($"File \"{file.FileName}\" exists already!");
                    }

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                return Ok($"File \"{file.FileName}\" saved.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{fileName}")]
        public async Task<IActionResult> DeletePdfDocument(string fileName)
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("No Company found!");
                }
                var path = MAIN_DIRECTORY + companyId;

                var filePath = Path.Combine(path, fileName);
                if (!System.IO.File.Exists(filePath))
                {
                    return BadRequest($"File \"{fileName}\" not found!");
                }

                System.IO.File.Delete(filePath);
                return Ok($"File \"{fileName}\" deleted.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<Guid> GetCompanyIdForUser()
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            if (user.Company != null)
            {
                return user.Company.Id;
            }

            return Guid.Empty;
        }
    }
}
