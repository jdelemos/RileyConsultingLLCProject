using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quick_Application2.Core.Infrastructure;
using Quick_Application2.Core.Models.Jms;

namespace Quick_Application2.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InmatesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public InmatesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // ============================================
        // 🔹 GET: api/v1/inmates
        // ============================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inmates = await _db.Inmates
                .Include(i => i.Jail)
                .Include(i => i.Cell)
                .OrderByDescending(i => i.BookingDate)
                .Select(i => new
                {
                    i.Id,
                    i.ExternalId,
                    i.FirstName,
                    i.LastName,
                    i.Status,
                    i.BookingDate,
                    Jail = i.Jail != null ? i.Jail.Name : "N/A",
                    Cell = i.Cell != null ? i.Cell.CellNumber : "N/A"
                })
                .ToListAsync();

            return Ok(inmates);
        }

        // ============================================
        // 🔹 GET: api/v1/inmates/{id}
        // ============================================
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var inmate = await _db.Inmates
                .Include(i => i.Jail)
                .Include(i => i.Cell)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inmate == null)
                return NotFound();

            return Ok(new
            {
                inmate.Id,
                inmate.ExternalId,
                inmate.FirstName,
                inmate.LastName,
                inmate.Status,
                inmate.BookingDate,
                Jail = inmate.Jail?.Name ?? "N/A",
                Cell = inmate.Cell?.CellNumber ?? "N/A",
                inmate.Notes,
                inmate.DateOfBirth
            });
        }

        // ============================================
        // 🔹 POST: api/v1/inmates
        // ============================================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Inmate inmate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            inmate.Id = Guid.NewGuid();
            inmate.BookingDate = DateTime.UtcNow;

            _db.Inmates.Add(inmate);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = inmate.Id }, inmate);
        }

        // ============================================
        // 🔹 PUT: api/v1/inmates/{id}
        // ============================================
        [HttpPut("{id:guid}")]
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

        // ============================================
        // 🔹 DELETE: api/v1/inmates/{id}
        // ============================================
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var inmate = await _db.Inmates.FindAsync(id);
            if (inmate == null)
                return NotFound();

            _db.Inmates.Remove(inmate);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        // ============================================
        // 🔹 GET: api/v1/inmates/search
        //      Supports frontend form-based filters
        // ============================================
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? externalId,
            [FromQuery] string? firstName,
            [FromQuery] string? lastName,
            [FromQuery] string? facility,
            [FromQuery] string? status,
            [FromQuery] DateTime? bookingDateFrom)
        {
            var query = _db.Inmates
                .Include(i => i.Jail)
                .Include(i => i.Cell)
                .AsQueryable();

            // External ID (e.g., SCMJ-001)
            if (!string.IsNullOrWhiteSpace(externalId))
                query = query.Where(i => i.ExternalId.ToLower().Contains(externalId.ToLower()));

            // Name filters
            if (!string.IsNullOrWhiteSpace(firstName))
                query = query.Where(i => i.FirstName.ToLower().Contains(firstName.ToLower()));

            if (!string.IsNullOrWhiteSpace(lastName))
                query = query.Where(i => i.LastName.ToLower().Contains(lastName.ToLower()));

            // Facility (by Jail Name, not GUID)
            if (!string.IsNullOrWhiteSpace(facility))
                query = query.Where(i => i.Jail != null && i.Jail.Name.ToLower().Contains(facility.ToLower()));

            // Status
            if (!string.IsNullOrWhiteSpace(status) && status.ToLower() != "all")
                query = query.Where(i => i.Status.ToLower() == status.ToLower());

            // Booking Date
            if (bookingDateFrom.HasValue)
                query = query.Where(i => i.BookingDate >= bookingDateFrom.Value);

            var results = await query
                .OrderByDescending(i => i.BookingDate)
                .Select(i => new
                {
                    i.Id,
                    i.ExternalId,
                    i.FirstName,
                    i.LastName,
                    i.Status,
                    i.BookingDate,
                    Jail = i.Jail != null ? i.Jail.Name : "N/A",
                    Cell = i.Cell != null ? i.Cell.CellNumber : "N/A"
                })
                .ToListAsync();

            return Ok(results);
        }
    }
}
