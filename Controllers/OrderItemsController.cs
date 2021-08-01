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
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly csDbContext _context;

        public OrderItemsController(csDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblOrderItem>>> GetTblOrderItems()
        {
            return await _context.TblOrderItems
            .Include(x => x.Product)
            .ToListAsync();
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblOrderItem>> GetTblOrderItem(int id)
        {
            var tblOrderItem = await _context.TblOrderItems.FindAsync(id);

            if (tblOrderItem == null)
            {
                return NotFound();
            }

            return tblOrderItem;
        }

        // PUT: api/OrderItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblOrderItem(int id, TblOrderItem tblOrderItem)
        {
            if (id != tblOrderItem.OrderItemId)
            {
                return BadRequest();
            }

            _context.Entry(tblOrderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblOrderItemExists(id))
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

        // POST: api/OrderItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblOrderItem>> PostTblOrderItem(TblOrderItem tblOrderItem)
        {
            _context.TblOrderItems.Add(tblOrderItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblOrderItem", new { id = tblOrderItem.OrderItemId }, tblOrderItem);
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblOrderItem(int id)
        {
            var tblOrderItem = await _context.TblOrderItems.FindAsync(id);
            if (tblOrderItem == null)
            {
                return NotFound();
            }

            _context.TblOrderItems.Remove(tblOrderItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblOrderItemExists(int id)
        {
            return _context.TblOrderItems.Any(e => e.OrderItemId == id);
        }
    }
}
