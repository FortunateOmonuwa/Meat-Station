using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public List<OrderDetails> Details { get; set; }
    }
}
