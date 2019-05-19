using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using noteApi.Models;
using Microsoft.EntityFrameworkCore;


namespace noteApi.Controllers
{
    [Route("api/note")]
    [ApiController]
    public class noteController : ControllerBase
    {
        private readonly noteContext _context;
        
        public noteController(noteContext context)
        {
            _context = context;
            if (_context.noteItems.Count() == 0)
            { 
            _context.noteItems.Add(new noteItem { Name = "Заметка" });
            _context.SaveChanges();
            }

        }

        //GET: api/note
        [HttpGet]
        public async Task<ActionResult<IEnumerable<noteItem>>> GetnoteItems()
        {
            return await _context.noteItems.ToListAsync();
        }

        //GET: api/note/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<noteItem>> GetnoteItem(long id)
        {
            var noteItem = await _context.noteItems.FindAsync(id);

            if (noteItem == null)
                {
                return NotFound();
                }
            return noteItem;
        }
        //POST : api/note
        [HttpPost]
        public async Task<ActionResult<noteItem>> PostnoteItem(noteItem item)
        {
            _context.noteItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetnoteItem), new { id = item.Id }, item);
        }

        //PUT : api/note/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutnoteItem(long id, noteItem item)
        {
            if ( id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        //DELETE :api/note/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletenoteItem(long id)
        {
            var noteItem = await _context.noteItems.FindAsync(id);
            
            if (noteItem == null)
            {
                return NotFound();
            }

            _context.noteItems.Remove(noteItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }






    }
}