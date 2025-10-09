using System;
using System.ComponentModel.DataAnnotations;

namespace Quick_Application2.Core.Models.Jms
{
    public class Bond
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required] public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = default!;

        public decimal Amount { get; set; }
        public bool IsPaid { get; set; }
        [MaxLength(100)] public string? Type { get; set; } // e.g., Cash, Surety, ROR
        public DateTime? PostedDate { get; set; }
    }
}
