using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        public string RoleName {  get; set; } = string.Empty;

    }
}
