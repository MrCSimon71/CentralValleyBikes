using Microsoft.AspNetCore.Mvc;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Services;
using CentralValleyBikes.Api.Extensions;
using CentralValleyBikes.Api.DTOs;
using AutoMapper;
using CentralValleyBikes.Domain.Filters;
using CentralValleyBikes.Api.Helpers;
using CentralValleyBikes.Api.Attributes;

namespace CentralValleyBikes.Api.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService<Product, int> _productService;
        private readonly IUriService _uriService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;

        public ProductsController(IProductService<Product, int> productService, IUriService uriService, IMapper mapper, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _uriService = uriService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: /products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([ModelBinder(BinderType = typeof(ProductFilterModelBinder))] SearchFilter searchFilter)
        {
            try
            {
                searchFilter.ApplyRules();

                var result = await _productService.GetAllAsync(searchFilter);

                var products = _mapper.Map<List<ProductDto>>(result.Data.ToList());

                var pagedReponse = PaginationHelper.CreatePagedReponse<ProductDto>(products, searchFilter, result.TotalRecords, _uriService, Request.Path.Value);

                return Ok(pagedReponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // GET /products/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _productService.GetByIdAsync(id);

                var product = _mapper.Map<ProductDto>(result);

                if (product != null)
                    return Ok(product);
                else
                    return NotFound("No data found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // POST /products
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] ProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var newProduct = await _productService.AddAsync(_mapper.Map<Product>(product));

                return Created(string.Format("{0}/{1}", "/products", newProduct.ProductId), newProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to save data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // PUT /products/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var response = await _productService.UpdateAsync(_mapper.Map<Product>(product));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to update data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // DELETE /products/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _productService.DeleteAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to delete data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }

        }
    }
}
