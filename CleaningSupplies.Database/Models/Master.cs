using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningSupplies.Database.Models
{
    public class Master : BaseEntity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Discontinue")]
        public bool IsDeleted { get; set; }
    }
}
