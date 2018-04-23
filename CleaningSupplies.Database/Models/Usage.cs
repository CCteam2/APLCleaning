using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningSupplies.Database.Models
{
    public class Usage: BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Item { get; set; }
        [Required]
        [Display(Name = "Quantity Modified")]
        public int Quantity_modified { get; set; }
        public virtual Master GetMasterT { get; set; }
    }
}
