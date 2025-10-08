using Quick_Application2.Core.Models.Jms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick_Application2.Core.Models.JMS
{
    public class Unit
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required] public string Name { get; set; } = string.Empty;

        // Relationship to Jail
        public Guid JailId { get; set; }
        public Jail Jail { get; set; } = default!;

        // One Unit has many Cells
        public ICollection<Cell> Cells { get; set; } = new List<Cell>();
    }

}
