using AutoMapper;
using Azure;
using Meat_Station.DataAccess.AppDataContext;
using Meat_Station.DataAccess.Interfaces;
using Meat_Station.Domain.DTOs;
using Meat_Station.Domain.DTOs.UserDTOs;
using Meat_Station.Domain.Models;
using Meat_Station.Service.AuthService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Meat_Station.DataAccess.Repositories
{
    public class RegisterService : IRegisterService
    {
        private readonly IAuthService _authService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        

        public RegisterService(IAuthService authService, DataContext context, IMapper mapper)
        {
            _authService = authService;
            _context = context;
            _mapper = mapper;

        }
        public async Task<User> RegisterUser(UserRegisterDTO newUserModel)
        {
            //Check Database to see if Username of Mail already exists
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == newUserModel.UserName || u.Mail == newUserModel.Mail);
            try
            {
                if (newUserModel == null)
                {
                    throw new ArgumentNullException(OpResponse.FailedMessage);
                }
                else if (!Regex.IsMatch(newUserModel.FirstName, "^[a-zA-Z]+$") || !Regex.IsMatch(newUserModel.LastName, "^[a-zA-Z]+$"))//Check to validate what the user inputs
                {
                    throw new ArgumentException($"{OpResponse.FailedMessage}\n\nFirstName or LastName should contain only alphabets.");
                }
                else if (user is not null) //Check to see if the user already exists
                {
                    throw new ArgumentException($"{OpResponse.FailedMessage = $"Username: {newUserModel.UserName} or Email: {newUserModel.Mail}  already exists! Please try again."}");
                }
                else if (newUserModel.Password != newUserModel.ConfirmPassword) //Check if password and confirm password are the same
                {
                    throw new Exception($"{OpResponse.FailedStatus} \n\n {OpResponse.FailedMessage = "Passwords do not match! Please try again"} ");
                }
                else
                {
                    string passwordHash = _authService.CreatePasswordHash(newUserModel.Password); //// create password hash using bcrypt

                    var userLocation = _mapper.Map<Location>(newUserModel.Location); //Map location 
                    await _context.Locations.AddRangeAsync(userLocation);// Save location to databse
                    await _context.SaveChangesAsync();//Save changes


                    // var userRole = _mapper.Map<List<Role>>(newUserModel.Roles); //Map create roles DTO to user
                

                    User newUser = new()
                    {
                        FirstName = newUserModel.FirstName,
                        LastName = newUserModel.LastName,
                        Name = newUserModel.FirstName + " " + newUserModel.LastName,
                        UserName = newUserModel.UserName,
                        PasswordHash =  passwordHash,
                        Location = userLocation,
                       
                        Phone = newUserModel.Phone,
                        Mail = newUserModel.Mail,
                        VerificationToken = _authService.CreateRandomVerificationToken(),
                        VerifiedAt = null,
                        CreatedAt = DateTime.Now,
                        LocationId = userLocation.Id,
                    };


                    
                    var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == "User");
                    if(role == null)
                    {
                        throw new ArgumentNullException();
                    }


                    newUser.Role = role;
                    newUser.RoleId = role.Id;
                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();
                    return newUser;
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} \n{ex.Source} \n{ex.InnerException} \n\n\n\n {OpResponse.FailedStatus}\n {OpResponse.FailedMessage}");
            }
        }

        public async Task<string> VerifyUser(UserRegisterTokenVerificationDTO token)
        {
            try
            {
                var userToken = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token.Token);
                if (userToken == null)
                {
                    throw new Exception($"{OpResponse.FailedStatus}  \n {OpResponse.FailedMessage = "Verification was not successful. Please check your token or try again"}");
                }
                else if (userToken.TokenExpiration < DateTime.Now)
                {
                    userToken.TokenExpiration = null;
                    throw new Exception($"{OpResponse.FailedStatus}  \n {OpResponse.FailedMessage = "Token expired! Please try again"}");
                }
                else
                {
                    userToken.VerifiedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                    return $"{OpResponse.SucessStatus}\n{OpResponse.SuccessMessage = "Verification was successful"}";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} \n{ex.Source} \n{ex.InnerException} \n\n\n\n {OpResponse.FailedStatus}\n {OpResponse.FailedMessage}");
            }
        }
        public Task<string> Login(UserLoginDTO loginDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUsers() => await _context.Users.Include(r => r.Role).ToListAsync();
    }
}
