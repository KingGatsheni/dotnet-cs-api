using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_cs_api.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace dotnet_cs_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly csDbContext _context;
        private readonly IWebHostEnvironment _webEnv;

        public ProductsController(csDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webEnv = webHostEnvironment;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblProduct>>> GetTblProducts()
        {
            return await _context.TblProducts.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblProduct>> GetTblProduct(int id)
        {
            var tblProduct = await _context.TblProducts.FindAsync(id);

            if (tblProduct == null)
            {
                return NotFound();
            }

            return tblProduct;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblProduct(int id, TblProduct tblProduct)
        {
            if (id != tblProduct.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(tblProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblProduct>> PostTblProduct([FromForm] TblProduct tblProduct)
        {
            if (tblProduct.Image != null)
            {
                var fileName = Path.GetFileName(tblProduct.Image.FileName);
                var fileNameToSave = "wwwroot/public/" + fileName;
                using (var stream = new FileStream(fileNameToSave, FileMode.Create))
                {
                    await tblProduct.Image.CopyToAsync(stream);
                }

                tblProduct.ProductName = tblProduct.ProductName;
                tblProduct.PackSize = tblProduct.PackSize;
                tblProduct.ProductPrice = tblProduct.ProductPrice;
                tblProduct.Quantity = tblProduct.Quantity;
                tblProduct.Discription = tblProduct.Discription;
                tblProduct.ProductImage = fileName;
                _context.TblProducts.Add(tblProduct);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTblProduct", new { id = tblProduct.ProductId }, tblProduct);
            }
            else
            {
                return BadRequest();
            }
        }

        //update qty value by bought amount per order
        [HttpPost]
        [Route("qty")]
        public ActionResult UpdateQty(TblProduct product)
        {
           var quantity = _context.TblProducts.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (quantity != null)
            {
                quantity.Quantity =  quantity.Quantity - product.Quantity;
                _context.SaveChanges();
                return Ok(quantity);
            }
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblProduct(int id)
        {
            var tblProduct = await _context.TblProducts.FindAsync(id);
            if (tblProduct == null)
            {
                return NotFound();
            }

            _context.TblProducts.Remove(tblProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool TblProductExists(int id)
        {
            return _context.TblProducts.Any(e => e.ProductId == id);
        }
    }
}
