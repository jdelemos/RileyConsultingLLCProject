using Quick_Application2.Core.Models.Jms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick_Application2.Core.Models.JMS
{
    public class Transfer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // References
        public Guid InmateId { get; set; }
        public Inmate Inmate { get; set; } = default!;

        public Guid FromJailId { get; set; }
        public Jail FromJail { get; set; } = default!;

        public Guid ToJailId { get; set; }
        public Jail ToJail { get; set; } = default!;

        public DateTime TransferDate { get; set; } = DateTime.UtcNow;
        public string Reason { get; set; } = string.Empty;
    }

}
