using Azure;
using Meat_Station.DataAccess.AppDataContext;
using Meat_Station.DataAccess.Helpers;
using Meat_Station.DataAccess.Interfaces;
using Meat_Station.Domain.DTOs;
using Meat_Station.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonPatchDocument = Microsoft.AspNetCore.JsonPatch.JsonPatchDocument;

namespace Meat_Station.DataAccess.Repositories
{
    public class UserRoleService : IUserRoleService
    {
        private readonly DataContext context;
        private readonly UserConfirmationHelper confirmUser;
        public UserRoleService(DataContext context, UserConfirmationHelper confirmationUser)
        {
            this.context = context;
            this.confirmUser = confirmationUser;
        }
        public async Task<List<Role>> GetAllRolesAsync()
        {
            var roles = await context.Roles.ToListAsync();
          
            try
            {

                return roles;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} \n{ex.Source} \n{ex.InnerException} \n\n\n\n {OpResponse.FailedStatus}\n {OpResponse.FailedMessage}");
            }
        }

        public async Task<User> UpdateUserRolePatchAsync(string userId, JsonPatchDocument roleId)
        {
         
            try
            {
                var user = await confirmUser.ConfirmUser(userId);
               

                roleId.ApplyTo(user);

               
                await context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} \n{ex.Source} \n{ex.InnerException} \n\n\n\n {OpResponse.FailedStatus}\n {OpResponse.FailedMessage}");
            }
        }
    }
}
