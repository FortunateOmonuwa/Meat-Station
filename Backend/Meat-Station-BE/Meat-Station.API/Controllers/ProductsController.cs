using AutoMapper;
using Meat_Station.DataAccess.Interfaces;
using Meat_Station.Domain.DTOs.ProductDTO;
using Meat_Station.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meat_Station.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
        }


        [HttpPost("Add-New-Product")]
        public async Task<IActionResult> AddNew([FromBody]ProductCreateDTO newProduct)
        {
            try
            {
                if(ModelState.IsValid)
                {
                  
                    var product = await _productService.CreateAsync(newProduct);

                    
                    return Ok(product);
                }
                else
                {
                    return BadRequest("Operation unsuccessful! Please check your input and try again");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}\n {ex.Source}\n {ex.InnerException}");
            }
        }

        [HttpGet("Get-All-Product")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var product = await _productService.GetAllAsync();
                if(product is null)
                {
                    return NotFound(product);
                }
                else
                {
                    return Ok(product);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}\n {ex.Source}\n {ex.InnerException}");
            }
        }

        [HttpGet("Get-Product-By-Id")]
        public async Task<IActionResult> GetById([FromQuery]string id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product is null)
                {
                    return NotFound(product);
                }
                else
                {
                    return Ok(product);
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}\n {ex.Source}\n {ex.InnerException}");
            }
        }

        [HttpDelete("Delete-Product")]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            try
            {
                var product = await _productService.DeleteAsync(id);
                if(product is false)
                {
                    return NotFound(product);
                }
                else
                {
                    return Ok("Successfully deleted");
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"{ex.Message}\n {ex.Source}\n {ex.InnerException}");
            }
        }


        [HttpGet("Get-Paginated")]
        public async Task<IActionResult> Pagination([FromQuery] int page_number, [FromQuery] int pageSize)
        {
            try
            {
                var products = _productService.GetAllPaginatedAsync(page_number, pageSize);
                if(products is null)
                {
                    return NotFound(products);
                }
                else
                {
                    return Ok(products);
                }

            }
            catch( Exception ex)
            {
                return BadRequest($"{ex.Message}\n {ex.Source}\n {ex.InnerException}");
            }
        }


        [HttpPut("Update-Products")]
        public async Task<IActionResult> UpdateExisting([FromBody] ProductCreateDTO update, [FromQuery]  string id)
        {
            try
            {
                var productToUpdate = await _productService.UpdateAsync(update, id);
                if(productToUpdate is null)
                {
                    return BadRequest(productToUpdate);
                }
                else
                {
                    return Ok(productToUpdate);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}\n {ex.Source}\n {ex.InnerException}");
            }
        }

    }
}
