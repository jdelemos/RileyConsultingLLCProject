using Quick_Application2.Core.Models.JMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Quick_Application2.Core.Models.Jms
{
    public class Inmate
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // External (public-facing) identifier, e.g., "SCMJ-001"
        [MaxLength(50)]
        public string ExternalId { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        // Administrative fields
        [MaxLength(10)]
        public string Status { get; set; } = "0"; // "0" for Active, "1" for Released, etc.

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [MaxLength(2000)]
        public string? Notes { get; set; } = string.Empty;

        // Foreign Keys
        [ForeignKey(nameof(Jail))]
        public Guid JailId { get; set; }

        [JsonIgnore]
        public Jail Jail { get; set; } = default!;

        [ForeignKey(nameof(Cell))]
        public Guid? CellId { get; set; }

        [JsonIgnore]
        public Cell? Cell { get; set; }

        // Transfers (in or out)
        [JsonIgnore]
        public ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
