using Catalogo_Blazor.Server.Context;
using Catalogo_Blazor.Server.Util;
using Catalogo_Blazor.Shared.Models;
using Catalogo_Blazor.Shared.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get([FromQuery] int? categoryId = null)
        {
            var productsQuery = _context.Products.AsNoTracking();

            if(categoryId != null)
                productsQuery = productsQuery.Where(x => x.CategoryId == categoryId);

            return await productsQuery.ToListAsync();
        }

        [HttpGet("pages")]
        public async Task<ActionResult<List<Product>>> Get([FromQuery] Pagination pagination, 
            [FromQuery] string? name = null)
        {
            var queryable = _context.Products.AsQueryable();
            if(!string.IsNullOrEmpty(name))
            {
                queryable = queryable.Where(x => x.Name.Contains(name));
            }

            await HttpContext.InserirParametroEmPageResponse(queryable, pagination.CountPerPage);

            return await queryable.Paginate(pagination).ToListAsync();
        }

        [HttpGet("{id}", Name = "GetProduto")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return new CreatedAtRouteResult("GetProduto", new { id = product.ProductId }, product); 
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Put(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var produto = new Product { ProductId = id };
            _context.Remove(produto);
            await _context.SaveChangesAsync();
            return Ok(produto);
        }
    }
}
