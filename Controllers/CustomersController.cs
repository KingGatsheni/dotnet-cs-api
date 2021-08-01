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


        // POST: api/Customers
        //Registers a customer and Hash password
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<TblCustomer>> Register(TblCustomer customer)
        {
            var user = _context.TblCustomers.SingleOrDefault(a => a.Email.Equals(customer.Email));
            customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
            _context.TblCustomers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(new { fullName = customer.FirstName + customer.LastName, email = customer.Email });
        }


        //Login method with hashed password
        [HttpPost]
        [Route("login")]
        public ActionResult<TblCustomer> Login(string email, string password, TblCustomer customer)
        {
            var account = checkUser(email, password);
            if (account != null)
            {
                return BadRequest("Email or Password is not valid");
            }
            else
            {
                HttpContext.Session.SetString("Email", email);
                return Ok(new { loggedAs = customer.Email });
            }

        }


        private TblCustomer checkUser(string email, string password)
        {
            var account = _context.TblCustomers.SingleOrDefault(a => a.Email.Equals(email));
            if (account != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }
            return null;
        }
    }
}
