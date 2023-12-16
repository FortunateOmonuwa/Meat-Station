using Meat_Station.DataAccess.AppDataContext;
using Meat_Station.Domain.DTOs;
using Meat_Station.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.DataAccess.Helpers
{
    public class UserConfirmationHelper
    {
        private readonly DataContext context;
        public UserConfirmationHelper(DataContext context)
        {
            this.context = context;
        }

        public async Task <User> ConfirmUser(string userId)
        {
            if (int.TryParse(userId, out int user_Id))
            {
                var user = await context
                    .Users
                    .FirstOrDefaultAsync(x => x.Id == user_Id);
                if (user != null)
                {

                    return user;
                }
                else
                {
                    throw new Exception($"{OpResponse.FailedStatus} \n\n {OpResponse.FailedMessage = $"User with id of {userId} doesn't exist... Please try again"} ");
                }
            }
            else
            {
                var user = await context
                   .Users
                   .FirstOrDefaultAsync(x => x.Name == userId || x.UserName == userId || x.Mail == userId);
                if (user != null)
                {

                    return user;
                }
                else
                {
                    throw new Exception($"{OpResponse.FailedStatus} \n\n {OpResponse.FailedMessage = $"User with id of {userId} doesn't exist... Please try again"} ");
                }
            }
        }
    }
}
