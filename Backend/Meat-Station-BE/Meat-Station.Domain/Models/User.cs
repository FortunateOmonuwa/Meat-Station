using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; } = string.Empty;

        public string PasswordHash { get; set; }
        //public byte[]? PasswordSalt { get; set; }

        public string? RefreshToken { get; set; }
        public string? VerificationToken
        {
            get
            {
                if (verificationToken != null && tokenExpiration.HasValue && DateTime.UtcNow >= tokenExpiration)
                {

                    verificationToken = string.Empty;
                    tokenExpiration = null;
                }
                return verificationToken;
            }
            set
            {
                verificationToken = value;
                tokenExpiration = DateTime.UtcNow.AddMinutes(10);
            }
        }
        public DateTime? VerifiedAt { get; set; }

        public string? PasswordResetToken
        {
            get
            {
                if (passwordResetToken != null && tokenExpiration.HasValue && DateTime.UtcNow >= tokenExpiration)
                {
                   
                    passwordResetToken = string.Empty;
                    tokenExpiration = null;
                }
                return passwordResetToken;
            }
            set
            {
                passwordResetToken = value;
                tokenExpiration = DateTime.UtcNow.AddMinutes(10);
            }
        }

        public DateTime? TokenExpiration
        {
            get { return tokenExpiration; }
            set { tokenExpiration = value; }
        }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [ForeignKey(nameof(Location))]
        public int? LocationId { get; set; }
        public Location? Location { get; set; }



        //Fields
        private DateTime? tokenExpiration;
        private string? verificationToken;
        private string? passwordResetToken;
    }
}
