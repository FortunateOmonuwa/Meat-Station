using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.DTOs.LocationDTO
{
    public class LocationCreateDTO
    {
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Area { get; set; }
        public string? Country { get; set; }

        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }
    }
}
