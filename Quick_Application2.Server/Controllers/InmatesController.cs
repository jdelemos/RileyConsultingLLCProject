using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quick_Application2.Core.Infrastructure;
using Quick_Application2.Core.Models.Jms;

namespace Quick_Application2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InmatesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public InmatesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: api/inmates
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inmates = await _db.Inmates
                .Include(i => i.Jail)
                .ToListAsync();

            return Ok(inmates);
        }

        // GET: api/inmates/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var inmate = await _db.Inmates
                .Include(i => i.Jail)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inmate == null)
                return NotFound();

            return Ok(inmate);
        }

        // POST: api/inmates
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Inmate inmate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _db.Inmates.Add(inmate);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = inmate.Id }, inmate);
        }

        // PUT: api/inmates/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Inmate updated)
        {
            if (id != updated.Id)
                return BadRequest("Mismatched inmate ID.");

            var existing = await _db.Inmates.FindAsync(id);
            if (existing == null)
                return NotFound();

            _db.Entry(existing).CurrentValues.SetValues(updated);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/inmates/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var inmate = await _db.Inmates.FindAsync(id);
            if (inmate == null)
                return NotFound();

            _db.Inmates.Remove(inmate);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
    [FromQuery] string? inmateId,
    [FromQuery] string? firstName,
    [FromQuery] string? lastName,
    [FromQuery] Guid? facilityId,
    [FromQuery] string? status,
    [FromQuery] DateTime? bookingDateFrom)
        {
            var query = _db.Inmates
                .Include(i => i.Jail)
                .AsQueryable();

            // 🔹 Filter by Inmate External ID
            if (!string.IsNullOrWhiteSpace(inmateId))
                query = query.Where(i => i.ExternalId.ToLower().Contains(inmateId.ToLower()));

            // 🔹 Filter by First Name
            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(i => i.FirstName.ToLower().Contains(firstName.ToLower()));

            // 🔹 Filter by Last Name
            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(i => i.LastName.ToLower().Contains(lastName.ToLower()));

            // 🔹 Filter by Facility (Jail)
            if (facilityId.HasValue)
                query = query.Where(i => i.JailId == facilityId.Value);

            // 🔹 Filter by Status
            if (!string.IsNullOrWhiteSpace(status) && status != "All")
                query = query.Where(i => i.Status.ToLower() == status.ToLower());

            // 🔹 Filter by Booking Date (from)
            if (bookingDateFrom.HasValue)
                query = query.Where(i => i.BookingDate >= bookingDateFrom.Value);

            var results = await query.ToListAsync();

            return Ok(results);
        }

    }
}
