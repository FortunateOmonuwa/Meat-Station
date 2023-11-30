using Meat_Station.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Meat_Station.Domain.DTOs.LocationDTO;
using Meat_Station.Domain.DTOs.Roles;

namespace Meat_Station.Domain.DTOs.UserDTOs
{
    public class UserRegisterDTO
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; } = string.Empty;
     
        [Required, MinLength(8, ErrorMessage = "Password has to be at least 8 Characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = " Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        //public List<RolesCreateDTO> Roles { get; set; }
        public LocationCreateDTO? Location { get; set; }
    }
}
