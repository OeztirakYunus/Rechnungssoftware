using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.Entities;
using CommonBase.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ProductsController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                var email = HttpContext.User.Identity.Name;
                var user = await _uow.UserRepository.GetUserByEmail(email);
                var products = await _uow.ProductRepository.GetAllAsync();
                products = products.Where(i => user.Company.Products.Any(a => a.Id.Equals(i.Id))).ToArray();

                return Ok(products);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            try
            {
                var guid = Guid.Parse(id);
                if (!await CheckAuthorization(guid))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to get this product!" });
                }

                var product = await _uow.ProductRepository.GetByIdAsync(guid);
                return Ok(product);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(Product product)
        {
            try
            {
                if (!await CheckAuthorization(product.Id))
                {
                    return Unauthorized(new { Status = "Error", Message = $"You are not allowed to update this product!" });
                }

                var entity = await _uow.ProductRepository.GetByIdAsync(product.Id);
                product.CopyProperties(entity);
                await _uow.ProductRepository.Update(entity);
                await _uow.SaveChangesAsync();
                return Ok(new { Status = "Success", Message = "Product updated." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> PostProduct(Product product)
        //{
        //    try
        //    {
        //        if (!await CheckAuthorization(product.Id))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to add this product!" });
        //        }

        //        await _uow.ProductRepository.AddAsync(product);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Product>> DeleteProduct(string id)
        //{
        //    try
        //    {
        //        var guid = Guid.Parse(id);
        //        if (!await CheckAuthorization(guid))
        //        {
        //            return Unauthorized(new { Status = "Error", Message = $"You are not allowed to delete this product!" });
        //        }

        //        await _uow.ProductRepository.Remove(guid);
        //        await _uow.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        private async Task<bool> CheckAuthorization(Guid productId)
        {
            var email = HttpContext.User.Identity.Name;
            var user = await _uow.UserRepository.GetUserByEmail(email);
            return user.Company.Products.Any(i => i.Id.Equals(productId));
        }
    }
}
