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
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService<Brand, int> _brandService;
        private readonly ILogger<BrandsController> _logger;
        private readonly IMapper _mapper;

        public BrandsController(IBrandService<Brand, int> brandService, IMapper mapper, ILogger<BrandsController> logger)
        {
            _brandService = brandService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: /brands
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var brands = await _brandService.GetAllAsync();

                return Ok(brands);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // GET /brands/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var brand = await _brandService.GetByIdAsync(id);

                if (brand != null)
                    return Ok(brand);
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
