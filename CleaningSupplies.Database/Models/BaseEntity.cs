using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningSupplies.Database.Models
{
    public class BaseEntity
    {
        [Required]
        public virtual ApplicationUser CreatedById { get; set; }
        //[Required]
        [Display(Name = "Created By DateTime")]
        public DateTimeOffset CreatedByDateTime { get; set; }
        //[Required]
        public virtual ApplicationUser ModifiedById { get; set; }
        //[Required]
        [Display(Name = "Modified By Datetime")]
        public DateTimeOffset ModifiedByDatetime { get; set; }
    }
}
