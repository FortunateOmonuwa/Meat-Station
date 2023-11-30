using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.DTOs.UserDTOs
{
    public class UserLoginDTO
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
