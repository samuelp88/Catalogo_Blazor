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
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Category>>> Get()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get([FromQuery] Pagination pagination,
            [FromQuery] string? name = null)
        {
            var queryable = _context.Categories.AsQueryable();

            if(!string.IsNullOrEmpty(name))
            {
                queryable = queryable.Where(c => c.Name.Contains(name));
            }

            await HttpContext.InserirParametroEmPageResponse(queryable, pagination.CountPerPage);

            return await queryable.Paginate(pagination).ToListAsync();
        }

        [HttpGet("{id}", Name ="GetCategoria")]
        public async Task<ActionResult<Category?>> Get(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("GetCategoria",
                new
                {
                   id = category.CategoryId,
                   category
                });
        }

        [HttpPut]
        public async Task<ActionResult<Category>> Put(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> Delete(int id)
        {
            var categoria = new Category { CategoryId = id };
            _context.Remove(categoria);
            await _context.SaveChangesAsync();
            return Ok(categoria);
        }
    }
}
