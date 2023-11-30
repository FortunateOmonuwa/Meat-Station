using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.DataAccess.Interfaces
{
    public interface IBaseService<T>
    {
        Task<T> CreateAsync(T entity, List<string> category_id);
        Task<T> UpdateAsync(T entity, string entity_id);
        Task<bool> DeleteAsync(string entity_id);
        Task<IQueryable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string entity_id);
       // Task<IQueryable<T>> GetAllSortedBySearchQueryAsync(string search_query, bool ascending);
        Task<IQueryable<T>> GetAllPaginatedAsync(int page_number, int page_size);
    }
}
