using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface;
using System.Net;

namespace PacaGroup.Ecommerce.Services.WebApi.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class Customers2Controller : ControllerBase
    {
        private readonly ICustomersApplication2 _customersApplication;

        public Customers2Controller(ICustomersApplication2 customersApplication)
        {
            _customersApplication = customersApplication;
        }

        [HttpPost("InsertAsync2")]
        public async Task<IActionResult> InsertAsync2([FromBody] CustomersDto2 customerDto)
        {
            if (customerDto == null)
                return BadRequest();

            var response = await _customersApplication.InsertAsync(customerDto);

            if (response.IsSuccess)
                return Ok(response);

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        [HttpPut("UpdateAsync/{customerId}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string customerId, [FromBody] CustomersDto2 customerDto)
        {
            if (customerDto == null)
                return BadRequest();

            if (!customerId.Equals(customerDto.CustomerId))
                return BadRequest();

            var response = await _customersApplication.UpdateAsync(customerDto);

            if (response.IsSuccess)
                return Ok(response);

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        [HttpDelete("DeleteAsync/{customerId}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = await _customersApplication.DeleteAsync(customerId);

            if (response.IsSuccess)
                return Ok(response);

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        [HttpGet("GetAsync/{customerId}")]
        public async Task<IActionResult> GetAsync([FromRoute] string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            var response = await _customersApplication.GetAsync(customerId);

            if (response.IsSuccess)
                return Ok(response);

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }

        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customersApplication.GetAllAsync();

            if (response.IsSuccess)
                return Ok(response);

            return StatusCode((int)HttpStatusCode.InternalServerError, response);
        }
    }
}
