using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.Service.AuthService.Interfaces
{
    public interface IAuthService
    {
        string CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, byte[] hash, byte[] salt);
        string CreateToken(string Id, string role);
        string CreateRandomVerificationToken();
    }
}
