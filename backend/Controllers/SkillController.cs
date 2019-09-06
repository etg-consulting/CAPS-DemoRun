using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caps.Models;

namespace Caps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly CapsContext _context;

        public SkillController(CapsContext context)
        {
            _context = context;

            if (_context.Skills.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Skills.Add(new Skill { Name = "Item1" });
                _context.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> List()
        {
            return await _context.Skills.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> Get(long id)
        {
            var todoItem = await _context.Skills.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<Skill>> Post(Skill item)
        {
            _context.Skills.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Skill item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var item = await _context.Skills.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Skills.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }                      
    }
}