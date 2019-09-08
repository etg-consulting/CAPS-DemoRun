using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caps.Models;

namespace Caps.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OpportunityController : ControllerBase
    {
        private readonly CapsContext _context;

        public OpportunityController(CapsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Opportunity>>> List()
        {
            return await _context.Opportunities.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Opportunity>> Get(long id)
        {
            var todoItem = await _context.Opportunities.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<Opportunity>> Post(Opportunity item)
        {
            _context.Opportunities.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Opportunity item)
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
            var item = await _context.Opportunities.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Opportunities.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }                      
    }
}