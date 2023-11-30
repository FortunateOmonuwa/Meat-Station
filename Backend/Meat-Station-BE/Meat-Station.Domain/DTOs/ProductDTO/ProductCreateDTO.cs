using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.DTOs.ProductDTO
{
    public class ProductCreateDTO
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, DataType(DataType.ImageUrl)]
        public string Image { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string? Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot 0.")]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }
        public List<string> CategoryName { get; set; }
    }
}
