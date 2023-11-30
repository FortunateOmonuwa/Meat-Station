using Meat_Station.DataAccess.AppDataContext;
using Meat_Station.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.DataAccess.Helpers
{
    public class CategoryHelper
    {
        private readonly DataContext _dataContext;

        public CategoryHelper(DataContext dataContext)
        {
            _dataContext = dataContext;   
        }
        public async Task<Category> CheckCategoryAsync( string category_id)
        {
            try
            {
           
                return await _dataContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == category_id);
                
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} \n{ex.Source} \n{ex.InnerException}");
            }
        }
    }

}
