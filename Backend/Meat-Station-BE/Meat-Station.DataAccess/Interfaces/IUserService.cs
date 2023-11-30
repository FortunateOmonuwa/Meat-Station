using Meat_Station.Domain.DTOs.UserDTOs;
using Meat_Station.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.DataAccess.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUser(UserRegisterDTO newUser);
        Task<string> VerifyUser(UserRegisterTokenVerificationDTO token);
        Task<string> Login(UserLoginDTO loginDetails);

    }
}
