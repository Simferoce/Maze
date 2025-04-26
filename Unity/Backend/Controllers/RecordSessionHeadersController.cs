using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordSessionHeadersController : ControllerBase
    {
        private readonly RecordSessionHeaderContext _context;

        public RecordSessionHeadersController(RecordSessionHeaderContext context)
        {
            _context = context;
        }

        // GET: api/RecordSessionHeaders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordSessionHeader>>> GetRecordSessionHeaders()
        {
            return await _context.RecordSessionHeaders.ToListAsync();
        }

        // GET: api/RecordSessionHeaders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecordSessionHeader>> GetRecordSessionHeader(long id)
        {
            var recordSessionHeader = await _context.RecordSessionHeaders.FindAsync(id);

            if (recordSessionHeader == null)
            {
                return NotFound();
            }

            return recordSessionHeader;
        }

        // POST: api/RecordSessionHeaders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecordSessionHeader>> PostRecordSessionHeader(RecordSessionHeader recordSessionHeader)
        {
            _context.RecordSessionHeaders.Add(recordSessionHeader);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecordSessionHeader", new { id = recordSessionHeader.Id }, recordSessionHeader);
        }

        // DELETE: api/RecordSessionHeaders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecordSessionHeader(long id)
        {
            var recordSessionHeader = await _context.RecordSessionHeaders.FindAsync(id);
            if (recordSessionHeader == null)
            {
                return NotFound();
            }

            _context.RecordSessionHeaders.Remove(recordSessionHeader);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecordSessionHeaderExists(long id)
        {
            return _context.RecordSessionHeaders.Any(e => e.Id == id);
        }
    }
}
