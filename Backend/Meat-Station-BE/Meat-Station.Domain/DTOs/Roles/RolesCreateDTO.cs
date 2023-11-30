using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.DTOs.Roles
{
    public class RolesCreateDTO
    {
        [DataType(DataType.Text)]
        public string RoleName { get; set; } = string.Empty;
    }
}
