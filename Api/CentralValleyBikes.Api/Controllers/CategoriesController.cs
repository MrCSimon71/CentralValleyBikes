using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CentralValleyBikes.Api.Extensions;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Services;
using CentralValleyBikes.Services;

namespace CentralValleyBikes.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService<Category, int> _categoryService;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService<Category, int> categoryService, IMapper mapper, ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: /categorys
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categories = await _categoryService.GetAllAsync();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // GET /categorys/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var category = await _categoryService.GetByIdAsync(id);

                if (category != null)
                    return Ok(category);
                else
                    return NotFound("No data found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }
    }
}
