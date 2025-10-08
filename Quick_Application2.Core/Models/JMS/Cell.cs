using Quick_Application2.Core.Models.Jms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick_Application2.Core.Models.JMS
{
    public class Cell
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required] public string CellNumber { get; set; } = string.Empty;

        public bool IsOccupied { get; set; }

        // Relationships
        public Guid UnitId { get; set; }
        public Unit Unit { get; set; } = default!;

        public Guid JailId { get; set; }
        public Jail Jail { get; set; } = default!;

        // One cell may contain multiple inmates (if dorm style)
        public ICollection<Inmate> Inmates { get; set; } = new List<Inmate>();
    }

}
