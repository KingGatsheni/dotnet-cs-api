using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_cs_api.Models;

namespace dotnet_cs_api.Controllers
{
    [Route("account/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly csDbContext _context;

        public CustomersController(csDbContext context)
        {
            _context = context;
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
                return Ok(new { fullName = customer.FirstName + customer.LastName, email = customer.Email });
            }
            else
            {
                return BadRequest("User With Same Email Already Exists");
            }

        }


        //Login method with hashed password
        // POST: account/Customers/login
        [HttpPost]
        [Route("login")]
        public ActionResult<TblCustomer> Login(TblCustomer customer)
        {
            var user = _context.TblCustomers.FirstOrDefault(c => c.Email.Equals(customer.Email));
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                if (!BCrypt.Net.BCrypt.Verify(customer.Password, user.Password))
                {
                    return Unauthorized();
                }
                else
                {
                    HttpContext.Session.SetString("Email", customer.Email);
                    return Ok(new { UserSessionEmail = HttpContext.Session.GetString("Email") });
                }
            }
        }

        // set the user session to logout
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            return Ok(new { loggedState = "user Signed out" });
        }

        //update user password if forgotten
        [HttpPost]
        [Route("update")]
        public IActionResult Update(TblCustomer customer)
        {
            var user = _context.TblCustomers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            if (user != null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password) ;
                _context.SaveChanges();
                return Ok(new {password = "password changed"});
            }

            return NoContent();
        }
    }
}
