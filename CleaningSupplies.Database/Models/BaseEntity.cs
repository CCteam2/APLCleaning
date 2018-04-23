using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningSupplies.Database.Models
{
    public class BaseEntity
    {
        public virtual ApplicationUser CreatedById { get; set; }
        public DateTimeOffset CreatedByDateTime { get; set; }
        public virtual ApplicationUser ModifiedById { get; set; }
        public DateTimeOffset ModifiedByDatetime { get; set; }
    }
}
