using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public FilesController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BSFile>>> GetFiles()
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("No Company found!");
                }
                var company = await _uow.CompanyRepository.GetByIdAsync(companyId);
                return Ok(company.Files);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byName/{fileName}")]
        public async Task<IActionResult> GetFileByName(string fileName)
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("No Company found!");
                }
                var company = await _uow.CompanyRepository.GetByIdAsync(companyId);
                var file = company.Files.Where(i => i.FileName.ToLower() == fileName.ToLower()).FirstOrDefault();

                if (file == null)
                {
                    return NotFound();
                }

                return File(file.Bytes, file.ContentType, file.FileName);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetFileById(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                var companyId = await GetCompanyIdForUser();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("No Company found!");
                }

                var company = await _uow.CompanyRepository.GetByIdAsync(companyId);
                var file = company.Files.Where(i => i.Id.Equals(guid)).FirstOrDefault();

                if(file == null)
                {
                    return NotFound();
                }

                return File(file.Bytes, file.ContentType, file.FileName);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostFile(IFormFile file)
        {
            try
            {
                var companyId = await GetCompanyIdForUser();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("No Company found!");
                }
               
                if (file.Length > 0)
                {
                    var fileEntity = new BSFile();
                    fileEntity.FileName = file.FileName;
                    fileEntity.ContentType = file.ContentType;
                    fileEntity.CreationTime = DateTime.Now;
                    fileEntity.CompanyId = companyId;

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        fileEntity.Bytes = stream.ToArray();
                    }

                    await _uow.BSFileRepository.AddAsync(fileEntity);
                    await _uow.SaveChangesAsync();
                }

                return Ok($"File \"{file.FileName}\" saved.");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                var companyId = await GetCompanyIdForUser();
                if (companyId.Equals(Guid.Empty))
                {
                    return BadRequest("No Company found!");
                }
                var company = await _uow.CompanyRepository.GetByIdAsync(companyId);
                var file = company.Files.Where(i => i.Id.Equals(guid)).FirstOrDefault();

                if (file == null)
                {
                    return NotFound();
                }

                await _uow.BSFileRepository.Remove(guid);
                await _uow.SaveChangesAsync();
                return Ok($"File deleted.");
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
