﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BillingSoftware.Core.Contracts;
using BillingSoftware.Core.DataTransferObjects.UserDtos;
using BillingSoftware.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BillingSoftware.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AuthController(IConfiguration configuration, UserManager<User> userManager, IUnitOfWork uow)
        {
            _uow = uow;
            _config = configuration;
            _userManager = userManager;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            try
            {
                var credentials = GetCredentials();
                var email = credentials[0];
                var password = credentials[1];

                var authenticatedUser = await _userManager.FindByEmailAsync(email);
                if (authenticatedUser == null || !await _userManager.CheckPasswordAsync(authenticatedUser, password))
                {
                    return Unauthorized(new { Status = "Error", Message = $"Wrong email or password!" });
                }

                var (token, roles) = await GenerateJwtToken(authenticatedUser);
                return Ok(new
                {
                    auth_token = new JwtSecurityTokenHandler().WriteToken(token),
                    userMail = authenticatedUser.Email,
                    userRoles = roles,
                    expiration = token.ValidTo
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Status = "Error", Message = $"Wrong email or password!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = "Error", Message = ex.Message });
            }
        }

        /// <summary>
        /// JWT erzeugen. Minimale Claim-Infos: Email und Rolle
        /// </summary>
        /// <param name="authenticatedUser"></param>
        /// <returns>Token mit Claims</returns>
        private async Task<(JwtSecurityToken, string)> GenerateJwtToken(User authenticatedUser)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var userRoles = await _userManager.GetRolesAsync(authenticatedUser);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, authenticatedUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Audience"],
              claims: authClaims,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: credentials);

            return (token, string.Join(',', userRoles));
        }

        private string[] GetCredentials()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            return credentials;
        }

        /// <summary>
        /// Neuen Benutzer registrieren.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto newUser)
        {
            var existingUser = await _userManager.FindByEmailAsync(newUser.User.Email);
            // gibt es schon einen Benutzer mit der Mailadresse?
            if (existingUser != null)
            {
                return BadRequest(new { Status = "Error", Message = "User already exists!" });
            }

            Company company = new Company()
            {
                Email = newUser.Company.Email,
                PhoneNumber = newUser.Company.PhoneNumber,
                CompanyName = newUser.Company.CompanyName,
                UstNumber = newUser.Company.UstNumber,
                Address = new Address()
                {
                    City = newUser.Company.Address.City,
                    Country = newUser.Company.Address.Country,
                    Street = newUser.Company.Address.Street,
                    ZipCode = newUser.Company.Address.ZipCode
                },
                BankInformation = new BankInformation()
                {
                    BankName = newUser.Company.BankInformation.BankName,
                    Iban = newUser.Company.BankInformation.Iban,
                    Bic = newUser.Company.BankInformation.Bic,
                }
            };

            User user = new User
            {
                Email = newUser.User.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = newUser.User.Email,
                Company = company,
                FirstName = newUser.User.FirstName,
                LastName = newUser.User.LastName
            };
            var resultUser = await _userManager.CreateAsync(user, newUser.User.Password);
            var resultRole = await _userManager.AddToRoleAsync(user, "Admin");

            if (!resultUser.Succeeded)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = string.Join(" ", resultUser.Errors.Select(e => e.Description))
                });
            }
            else if (!resultRole.Succeeded)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = string.Join(" ", resultRole.Errors.Select(e => e.Description))
                });
            }


            var (token, roles) = await GenerateJwtToken(user);
            return Ok(new { Status = "Ok", Message = $"User {user.Email} successfully added. Token: {new JwtSecurityTokenHandler().WriteToken(token)}" });
        }
    }
}
