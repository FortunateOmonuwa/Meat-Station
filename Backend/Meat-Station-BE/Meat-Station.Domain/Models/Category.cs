using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        public string CategoryName { get; set; } = string.Empty;
        public List<ProductCategories>? Products { get; set; }
    }
}
