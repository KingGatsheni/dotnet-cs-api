using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_cs_api.Models;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_cs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly csDbContext _context;

        public OrdersController(csDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblOrder>>> GetTblOrders()
        {
            return await _context.TblOrders
            .Include(x => x.OrderItems)
            .ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblOrder>> GetTblOrder(int id)
        {
            var tblOrder = await _context.TblOrders.FindAsync(id);

            if (tblOrder == null)
            {
                return NotFound();
            }

            return tblOrder;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblOrder(int id, TblOrder tblOrder)
        {
            if (id != tblOrder.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(tblOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblOrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblOrder>> PostTblOrder(TblOrder tblOrder)
        {
            _context.TblOrders.Add(tblOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblOrder", new { id = tblOrder.OrderId }, tblOrder);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblOrder(int id)
        {
            var tblOrder = await _context.TblOrders.FindAsync(id);
            if (tblOrder == null)
            {
                return NotFound();
            }

            _context.TblOrders.Remove(tblOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblOrderExists(int id)
        {
            return _context.TblOrders.Any(e => e.OrderId == id);
        }
    }
}
