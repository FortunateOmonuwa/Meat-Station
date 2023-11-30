using AutoMapper;
using Meat_Station.DataAccess.AppDataContext;
using Meat_Station.DataAccess.Helpers;
using Meat_Station.DataAccess.Interfaces;
using Meat_Station.Domain.DTOs;
using Meat_Station.Domain.DTOs.ProductDTO;
using Meat_Station.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Meat_Station.DataAccess.Repositories
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly CategoryHelper _confirmCategory;
        private readonly IMapper _mapper;
        public ProductService(DataContext dataContext, IMapper mapper, CategoryHelper confirmCategory)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _confirmCategory = confirmCategory;
        }
        public async Task<Product> CreateAsync(ProductCreateDTO new_product)
        {
            try
            {
                if (new_product == null)
                {
                    throw new ArgumentNullException($"{OpResponse.FailedMessage}");
                }
                else if (!Regex.IsMatch(new_product.Name, "^[a-zA-Z -_]+$"))
                {
                    throw new ArgumentException($"{OpResponse.FailedMessage = "Product name cannot contain numbers"}");
                }

                var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Name == new_product.Name);

                if (product != null)
                {
                    throw new ArgumentException($"{OpResponse.FailedMessage = $"{new_product.Name} already exists!"}");
                }

                var newProduct = new Product
                {
                    Name = new_product.Name,
                    Description = new_product.Description,
                    Image = new_product.Image,
                    Price = new_product.Price,
                    Stock = new_product.Stock,
                    Categories = new List<ProductCategories>()
                };

                await _dataContext.Products.AddAsync(newProduct);

                if (new_product.CategoryName != null)
                {
                    foreach (var categoryName in new_product.CategoryName)
                    {
                        var category = await _confirmCategory.CheckCategoryAsync(categoryName);

                        if (category == null)
                        {
                            category = new Category { CategoryName = categoryName };
                            await _dataContext.Categories.AddAsync(category);
                        }

                        var productCategory = new ProductCategories
                        {
                            Product = newProduct,
                            Category = category,
                        };

                        newProduct.Categories.Add(productCategory);
                        
                    }
                }

                await _dataContext.SaveChangesAsync();
                return newProduct;
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                throw new InvalidOperationException($"{ex.Message} \n{ex.Source} \n{ex.InnerException}");
            }
        }

        public async Task<Product> UpdateAsync(ProductCreateDTO updated_product, string product_id)
        {
            try
            {
                var product = await GetByIdAsync(product_id) ?? throw new ArgumentNullException("Product doesn't exist");
                if (!Regex.IsMatch(updated_product.Name, "^[a-zA-Z -_]+$"))
                {
                    throw new ArgumentException("Product name cannot contain numbers");
                }

                product.Name = updated_product.Name;
                product.Description = updated_product.Description;
                product.Image = updated_product.Image;
                product.Price = updated_product.Price;
                product.Stock = updated_product.Stock;

                // Load the existing categories
                _dataContext.Entry(product).Collection(p => p.Categories).Load();

                // Clear existing associations
                product.Categories.Clear();

                if (updated_product.CategoryName != null)
                {
                    foreach (var categoryName in updated_product.CategoryName)
                    {
                        var category = await _confirmCategory.CheckCategoryAsync(categoryName);
                        if (category == null)
                        {
                            var newCategory = new Category { CategoryName = categoryName };
                            _dataContext.Categories.Add(newCategory);
                            await _dataContext.SaveChangesAsync(); 
                            category = newCategory;
                        }
                        product.Categories.Add(new ProductCategories { Category = category });
                    }
                }

                _dataContext.Products.Update(product);
                await _dataContext.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                
                throw new InvalidOperationException($"{ex.Message} \n{ex.Source} \n{ex.InnerException}");
            }
        }



        public async Task<bool> DeleteAsync(string product_id)
        {

            try
            {
                if(int.TryParse(product_id, out int productId))
                {
                    var product = await _dataContext.Products
                        .FirstOrDefaultAsync(p => p.Id == productId);
                    if(product is null)
                    {
                        throw new ArgumentNullException($"Product with Id: {productId} doesn't exist!");
                    }
                    else
                    {
                        _dataContext.Products.Remove(product);
                        await _dataContext.SaveChangesAsync();
                        return true;
                    }
                }
                else
                {
                    var product = await _dataContext.Products
                        .FirstOrDefaultAsync(p => p.Name == product_id);
                    if(product is null)
                    {
                        throw new ArgumentNullException($" {product_id} doesn't exist!");
                    }
                    else
                    {
                        _dataContext.Products.Remove(product);
                        await _dataContext.SaveChangesAsync();
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"{ex.Message} \n{ex.Source} \n{ex.InnerException}");
            }
        }

        public async Task<IQueryable<Product>> GetAllAsync()
        {
            try
            {
                var products = await _dataContext
                    .Products
                    .Include(c => c.Categories)
                    .ToListAsync();
                if (!products.Any())
                {
                    throw new ArgumentNullException($"No products were found");
                }
                else
                {
                    return products.AsQueryable();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{ex.Message} \n{ex.Source} \n{ex.InnerException}");
            }
        }
        public async Task<Product> GetByIdAsync(string product_id)
        {
            try
            {
                if(int.TryParse(product_id, out int productid))
                {
                    var product = await _dataContext.Products
                        .FirstOrDefaultAsync(p => p.Id == productid);

                    
                    if(product is null)
                    {
                        throw new ArgumentNullException($"Product with Id: {product_id} is null");
                    }
                    else
                    {
                       return product;
                    }
                }
                else
                {
                    var product = await _dataContext.Products
                        .FirstOrDefaultAsync(p => p.Name == product_id);
                    if(product is null)
                    {
                        throw new ArgumentNullException($" {product_id} doesn't exist!");
                    }
                    else
                    {
                        return product;
                    }
                }
            }   
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{ex.Message} \n{ex.Source} \n{ex.InnerException}");
            }
        }

        
        public async Task<IQueryable<Product>> GetAllPaginatedAsync(int page_number, int page_size)
        {
            try
            {
                if (page_number < 1 || page_size < 10 || page_size > 10)
                {
                    page_number = 1;
                    page_size = 10;
                }

                var products = _dataContext.Products
                    .Skip((page_number - 1) * page_size)
                    .Take(page_size)
                    .AsQueryable();

                if (!products.Any())
                {
                    throw new ArgumentNullException("No products were found");
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"{ex.Message} \n{ex.Source} \n{ex.InnerException}");
            }
        }

        //public Task<IQueryable<Product>> GetAllSortedBySearchQueryAsync(string search_query, bool Ascending)
        //{
        //    try
        //    {
        //        Expression<Func<Product, bool>> searchQuery = products =>
        //        products.Name.Contains(search_query) ||
        //        products.

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"{ex.Message} \n{ex.Source} \n{ex.InnerException}");
        //    }
        //}
        
    }
}
