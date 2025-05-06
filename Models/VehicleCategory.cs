using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Vehicle.DataAccessLayer.Models
{
    public class VehicleCategory
    {
        public VehicleCategory()
        {
            VehicleProducts = new HashSet<VehicleProduct>();
        }
        [Required]
        [StringLength(10, ErrorMessage = "Category name cannot be longer than 10 characters.")]
        public string CategoryName {get;set;}
        [Key]
        public byte CategoryId { get; set; }
        public string CategoryDesciption { get; set; }

        public virtual ICollection<VehicleProduct> VehicleProducts { get; set; }
    }
}
