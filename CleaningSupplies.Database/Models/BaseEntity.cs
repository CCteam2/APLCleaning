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
        [Display(Name = "Created By")]
        public virtual ApplicationUser CreatedById { get; set; }

        [Display(Name = "Created By DateTime")]
        public DateTime? CreatedByDateTime { get; set; }
        
        [Display(Name = "Modified By")]
        public virtual ApplicationUser ModifiedById { get; set; }
        
        [Display(Name = "Modified By Datetime")]
        public DateTime? ModifiedByDatetime { get; set; }
    }
}
