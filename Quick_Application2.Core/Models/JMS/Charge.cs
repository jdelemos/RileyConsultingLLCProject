using System;
using System.ComponentModel.DataAnnotations;

namespace Quick_Application2.Core.Models.Jms
{
    public class Charge
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required] public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = default!;

        [MaxLength(200)] public string Offense { get; set; } = string.Empty;
        public decimal? BondAmount { get; set; }
        [MaxLength(100)] public string? Statute { get; set; }

        public DateTime FiledDate { get; set; } = DateTime.UtcNow;
    }
}
