using Quick_Application2.Core.Models.JMS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Quick_Application2.Core.Models.Jms
{
    public class Jail
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(80)]
        public string City { get; set; } = string.Empty;

        [MaxLength(2)]
        public string State { get; set; } = "CA";

        [MaxLength(10)]
        public string Zip { get; set; } = string.Empty;

        public int OpenedYear { get; set; }                // ✅ Added back
        public int Capacity { get; set; }
        public JailType Type { get; set; } = JailType.CountyJail;
        public SecurityLevel Security { get; set; } = SecurityLevel.MultiLevel;
        public JailStatus Status { get; set; } = JailStatus.Operational;

        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string FeaturesCsv { get; set; } = string.Empty;

        // Navigation Properties
        [JsonIgnore]
        public ICollection<Unit> Units { get; set; } = new List<Unit>();

        [JsonIgnore]
        public ICollection<Cell> Cells { get; set; } = new List<Cell>();

        [JsonIgnore]
        public ICollection<Inmate> Inmates { get; set; } = new List<Inmate>();

        [JsonIgnore]
        public ICollection<Transfer> Transfers { get; set; } = new List<Transfer>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
