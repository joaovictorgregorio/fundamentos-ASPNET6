using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("categories")]
        public async Task<IActionResult> GetAsync([FromServices]BlogDataContext context)
        {
            var categories = await context.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] BlogDataContext context, [FromRoute] int id)
        {
            try
            {
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE7 - Falha interna no servidor");
            }
        }

        [HttpPost("categories")]
        public async Task<IActionResult> PostAsync([FromServices] BlogDataContext context, [FromBody] CreateCategoryViewModel model)
        {
            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"categories/{category.Id}", category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE1 - Não foi possível incluir a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE2 - Falha interna no servidor");
            }
        }

        [HttpPut("categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] BlogDataContext context, 
            [FromRoute] int id, 
            [FromBody] Category model)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                category.Name = model.Name;
                category.Slug = model.Slug;

                context.Categories.Update(category);
                await context.SaveChangesAsync();

                return Ok(model);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE3 - Não foi possível atualizar a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE4 - Falha interna no servidor");
            }
        }

        [HttpDelete("categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromServices] BlogDataContext context, [FromRoute] int id)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                context.Categories.Remove(category);
                await context.SaveChangesAsync();

                return Ok(category);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "05XE5 - Não foi possível excluir a categoria");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "05XE6 - Falha interna no servidor");
            }
        }
    }
}
