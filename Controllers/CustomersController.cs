using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_cs_api.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace dotnet_cs_api.Controllers
{
    [Route("account/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly csDbContext _context;
        public IConfiguration _iconfiguration;

        public CustomersController(IConfiguration configuration, csDbContext context)
        {
            _context = context;
            _iconfiguration = configuration;
        }


        // POST: account/Customers/register
        //Registers a customer and Hash password
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<TblCustomer>> Register(TblCustomer customer)
        {
            if (!_context.TblCustomers.Any(a => a.Email == customer.Email))
            {
                customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
                _context.TblCustomers.Add(customer);
                await _context.SaveChangesAsync();
                return Ok(customer);
            }
            else
            {
                return BadRequest("User With Same Email Already Exists");
            }

        }


        //Login method with hashed password
        // POST: account/Customers/login
        [HttpPost]
        [Route("auth")]
        public ActionResult<TblCustomer> Login(TblCustomer customer)
        {

            if (customer != null && customer.Email != null)
            {
                var user = _context.TblCustomers.FirstOrDefault(c => c.Email.Equals(customer.Email));
                if (user == null)
                {
                    return BadRequest("User Does Not Exist");
                }
                else
                {
                    if (!BCrypt.Net.BCrypt.Verify(customer.Password, user.Password))
                    {
                        return BadRequest("Invalid Credentials");
                    }
                    else
                    {
                        IdentityOptions _options = new IdentityOptions();
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration["Jwt:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                           claims: new Claim[]{
                                new Claim(JwtRegisteredClaimNames.Sub, user.CustomerId.ToString()),
                                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                                new Claim(JwtRegisteredClaimNames.Sub, user.LastName),
                                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                                new Claim(JwtRegisteredClaimNames.Sub, user.PhoneNumber),
                                new Claim(JwtRegisteredClaimNames.Sub, user.Residence),
                           },
                            expires: DateTime.Now.AddMinutes(60),
                            signingCredentials: credentials);
                        var genToken = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(new { Authentication = true, Token = genToken });
                    }

                }
            }
            else
            {
                return BadRequest();
            }

        }

        //update user password if forgotten
        [HttpPost]
        [Route("update")]
        public IActionResult Update(TblCustomer customer)
        {
            var user = _context.TblCustomers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            if (user != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
                _context.SaveChanges();
                return Ok(new { password = "password changed" });
            }

            return NoContent();
        }
    }
}
