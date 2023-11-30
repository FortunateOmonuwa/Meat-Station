using BCrypt.Net;
using Meat_Station.Service.AuthService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Service.AuthService.Repository
{
    public class AuthService : IAuthService
    {
        public string CreatePasswordHash(string password)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentNullException(password);
                }
                else
                {
                    string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                    return passwordHash;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} \n{ex.Source} \n{ex.InnerException}");
            }
        }

        public string CreateRandomVerificationToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(8));
        }

        public string CreateToken(string Id, string role)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
        {
            throw new NotImplementedException();
        }
    }
}
