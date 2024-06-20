using Microsoft.AspNetCore.Mvc;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Domain.Services;
using CentralValleyBikes.Api.Extensions;
using CentralValleyBikes.Api.DTOs;
using AutoMapper;
using CentralValleyBikes.Api.Helpers;
using CentralValleyBikes.Domain.Filters;

namespace CentralValleyBikes.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService<Customer, int> _customerService;
        private readonly IUriService _uriService;
        private readonly ILogger<CustomersController> _logger;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService<Customer, int> customerService, IUriService uriService, IMapper mapper, ILogger<CustomersController> logger)
        {
            _customerService = customerService;
            _uriService = uriService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: /customers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([ModelBinder(BinderType = typeof(CustomerFilterModelBinder))] SearchFilter searchFilter)
        {
            try
            {
                searchFilter.ApplyRules();

                var result = await _customerService.GetAllAsync(searchFilter);

                var customers = _mapper.Map<List<CustomerDto>>(result.Data.ToList());

                var pagedReponse = PaginationHelper.CreatePagedReponse<CustomerDto>(customers, searchFilter, result.TotalRecords, _uriService, Request.Path.Value);

                return Ok(pagedReponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // GET /customers/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);

                if (customer != null)
                    return Ok(customer);
                else
                    return NotFound("No data found");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected in GET call: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // POST /customers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CustomerDto customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var newCustomer = await _customerService.AddAsync(_mapper.Map<Customer>(customer));

                return Created(string.Format("{0}/{1}", "/customers", newCustomer.CustomerId), newCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to save data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // PUT /customers/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, [FromBody] CustomerDto customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var response = await _customerService.UpdateAsync(_mapper.Map<Customer>(customer));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error detected while trying to update data: {ex}");
                return this.InternalServerError(ex.HResult, ex.Message);
            }
        }

        // DELETE /customers/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _customerService.DeleteAsync(id);

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
