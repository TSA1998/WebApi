using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestApi.Models;

namespace RestApi.Controllers
{
    [Route("api/rest")]
    [ApiController]
    public class RestController : ControllerBase
    {
        private readonly RestContext _context;

        public RestController(RestContext context)
        {
            _context = context;

            if (_context.DishItems.Count() == 0)
            {
                _context.DishItems.Add(new DishItem { Name = "Суп", Description = "Вкусный", Price = 100 });
                _context.DishItems.Add(new DishItem { Name = "Лапша", Description = "Сытная", Price = 199 });
                _context.SaveChanges();
            }
        }

        // GET: api/Rest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishItem>>> GetDishItems()
        {
            return await _context.DishItems.ToListAsync();
        }

        // GET: api/Rest/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DishItem>> GetDishItem(long id)
        {
            var DishItem = await _context.DishItems.FindAsync(id);

            if (DishItem == null)
            {
                return NotFound();
            }

            return DishItem;
        }


        // POST: api/Rest
        [HttpPost]
        public async Task<ActionResult<DishItem>> PostDishItem(DishItem item)
        {
            _context.DishItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDishItem), new { id = item.Id }, item);
        }


        // PUT: api/Rest/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDishItem(long id, DishItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/Rest/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDishItem(long id)
        {
            var DishItem = await _context.DishItems.FindAsync(id);

            if (DishItem == null)
            {
                return NotFound();
            }

            _context.DishItems.Remove(DishItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}