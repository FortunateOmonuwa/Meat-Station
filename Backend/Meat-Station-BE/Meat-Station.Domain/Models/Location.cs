using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Area { get; set; }
        public string? Country { get; set; }

        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }
    }
}
