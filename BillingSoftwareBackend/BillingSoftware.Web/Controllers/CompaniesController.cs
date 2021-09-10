using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.DataTransferObjects;
using BillingSoftware.Core.Entities;
using BillingSoftware.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            return await _context.Companies
                .Include(u => u.Users)
                .Include(a => a.Addresses)
                .Include(c => c.Contacts)
                .Include(o => o.Offers)
                .Include(o => o.OrderConfirmations)
                .Include(d => d.DeliveryNotes)
                .Include(i => i.Invoices)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _context.Companies
                //.Include(u => u.Users)
                //.Include(a => a.Addresses)
                //.Include(c => c.Contacts)
                //.Include(o => o.Offers)
                //.Include(o => o.OrderConfirmations)
                //.Include(d => d.DeliveryNotes)
                //.Include(i => i.Invoices)
                //.Where(e => e.Id == id)
                //.FirstOrDefaultAsync();
                .FindAsync(id);
            if(company.Users == null)
            {
                System.Console.WriteLine("Is null");
            }
            else
            {
                System.Console.WriteLine(company.Users.First().FirstName);
            }

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            if (id != company.Id)
            {
                return BadRequest();
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("add-user")]
        public async Task<IActionResult> AddUserToCompany(CompanyAndUserDto companyAndUserDto)
        {
            if(companyAndUserDto == null || companyAndUserDto.Company == null || companyAndUserDto.User == null)
            {
                return BadRequest("User or Company is null.");
            }

            var company = companyAndUserDto.Company;
            var user = companyAndUserDto.User;

            if(company.Users == null)
            {
                company.Users = new List<User>();
            }

            company.AddUser(user);
            _context.Entry(company).State = EntityState.Modified;
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> DeleteCompany(int id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return company;
        }

        private bool CompanyExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}
