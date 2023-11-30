using Meat_Station.Domain.DTOs.ProductDTO;
using Meat_Station.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meat_Station.DataAccess.Interfaces
{
    public interface IProductService 
    {
        Task<Product> CreateAsync(ProductCreateDTO newProduct);
        Task<Product> UpdateAsync(ProductCreateDTO entity, string entity_id);
        Task<bool> DeleteAsync(string entity_id);
        Task<IQueryable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string entity_id);
        // Task<IQueryable<T>> GetAllSortedBySearchQueryAsync(string search_query, bool ascending);
        Task<IQueryable<Product>> GetAllPaginatedAsync(int page_number, int page_size);
    }
}
