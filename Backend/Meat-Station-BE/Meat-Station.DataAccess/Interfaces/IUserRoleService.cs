using Azure;
using Meat_Station.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonPatchDocument = Microsoft.AspNetCore.JsonPatch.JsonPatchDocument;


namespace Meat_Station.DataAccess.Interfaces
{
   public interface IUserRoleService
    {
        Task<List<Role>> GetAllRolesAsync();
        Task<User> UpdateUserRolePatchAsync( string userId, JsonPatchDocument roleId);

    };
}
