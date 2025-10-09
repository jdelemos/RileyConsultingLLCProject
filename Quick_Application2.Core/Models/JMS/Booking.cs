using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Quick_Application2.Core.Models.Jms
{
    public class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Keys
        [Required]
        public Guid InmateId { get; set; }

        [Required]
        public Guid JailId { get; set; }

        // Navigation Properties
        public Inmate Inmate { get; set; } = default!;
        public Jail Jail { get; set; } = default!;

        // Booking lifecycle
        public DateTime IntakeDate { get; set; } = DateTime.UtcNow;
        public DateTime? ReleaseDate { get; set; }
        public string? ReleaseReason { get; set; }

        // Charges and related data
        public ICollection<Charge> Charges { get; set; } = new List<Charge>();
        public ICollection<Hold> Holds { get; set; } = new List<Hold>();
        public Bond? Bond { get; set; }

        // Status flag for convenience
        public BookingStatus Status { get; set; } = BookingStatus.Active;
    }
}
