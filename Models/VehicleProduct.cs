using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CodeFirst.Vehicle.DataAccessLayer.Models
{
    public class VehicleProduct
    {
        [Key]
        public String ProductId { get; set; }
        [Required]
        [StringLength(20)]
        public string ProductName { get; set; }
        [MinLength(0), MaxLength(30)]
        public string ProductDescription { get; set; }
        [Required]
        [Range(0, 1000000, ErrorMessage = "Price must be between 0 and 1,000,000.")]
        [Column(TypeName ="Numeric(8)")]
        public decimal ProductPrice { get; set; }
        public byte  CategoryId { get; set; }

        public VehicleCategory VehicleCategory { get; set; }

    }
}
