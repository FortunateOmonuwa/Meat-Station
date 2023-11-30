using Meat_Station.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.DTOs.ProductDTO
{
    public class ProductGetDTO
    {
        [Key]
        public int Id { get; set; }

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
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public bool IsAvailable => Stock > 0;
        
    }
}
