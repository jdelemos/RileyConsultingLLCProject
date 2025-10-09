using System;
using System.ComponentModel.DataAnnotations;

namespace Quick_Application2.Core.Models.Jms
{
    public class Hold
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required] public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = default!;

        [MaxLength(200)] public string Agency { get; set; } = string.Empty;
        [MaxLength(500)] public string Reason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
