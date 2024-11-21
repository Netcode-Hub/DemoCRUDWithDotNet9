using DemoCRUDWithDotNet9.Data;
using DemoCRUDWithDotNet9.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoCRUDWithDotNet9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ProductDbContext context) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await context.Products.AsNoTracking().ToListAsync();
            return products.Count != 0 ? Ok(products) : NotFound();
        }

        [HttpGet("single/{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            return product is not null ? Ok(product) : NotFound();
        }

        [HttpPost("add")]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            product.DateCreated = DateTime.Now;
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            
        }

        [HttpPut("update")]
        public async Task<ActionResult<Product>> UpdateProduct(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return Ok(product);
            //var _product = await context.Products.FindAsync(product.Id);
            //if (_product != null)
            //{
            //    _product.Description = product.Description;
            //    _product.Price = product.Price;
            //}
            //return BadRequest();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var _product = await context.Products.FindAsync(id);
            if (_product != null)
            {
                context.Products.Remove(_product);
                await context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
