using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.DTOs
{
    public static class OpResponse
    {
        public static string SucessStatus { get; set; } = "Successful";
        public static string SuccessMessage { get; set; } = "Operation was succesful!";
        public static string FailedStatus { get; set; } = "Error";
        public static string FailedMessage { get; set; } = "Something went wrong! Please try again";
    }
}
