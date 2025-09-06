using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface.UseCases;
using PacaGroup.Ecommerce.Transversal.Common;
using Swashbuckle.AspNetCore.Annotations;

namespace PacaGroup.Ecommerce.Services.WebApi.Controllers.V2
{
    [Authorize]
    [EnableRateLimiting("fixedWindow")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [SwaggerTag("Manage Discounts of Products")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountsApplication _discountsApplication;

        public DiscountsController(IDiscountsApplication discountsApplication)
        {
            _discountsApplication = discountsApplication;
        }

        [HttpPost("Create")]
        [SwaggerOperation(
            Summary = "Create Discount",
            Description = "Creates a new discount",
            OperationId = "CreateDiscount",
            Tags = new[] { "Discounts" })]
        [SwaggerResponse(200, "Discount created successfully", typeof(Response<DiscountDto>))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<IActionResult> Create([FromBody] DiscountDto discountDto)
        {
            if (discountDto == null)
                return BadRequest();

            var response = await _discountsApplication.Create(discountDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPut("Update/{id}")]
        [SwaggerOperation(
            Summary = "Update Discount",
            Description = "Updates an existing discount",
            OperationId = "UpdateDiscount",
            Tags = new[] { "Discounts" })]
        [SwaggerResponse(200, "Discount updated successfully", typeof(Response<DiscountDto>))]
        [SwaggerResponse(404, "Discount not found")]
        public async Task<IActionResult> Update(int id, [FromBody] DiscountDto discountDto)
        {
            var discountExists = await _discountsApplication.Get(id);
            if (discountExists.Data == null)
                return NotFound(discountExists);

            if (discountDto == null)
                return BadRequest();

            var response = await _discountsApplication.Update(discountDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(
            Summary = "Delete Discount",
            Description = "Deletes a discount by ID",
            OperationId = "DeleteDiscount",
            Tags = new[] { "Discounts" })]
        [SwaggerResponse(200, "Discount deleted successfully", typeof(Response<bool>))]
        [SwaggerResponse(404, "Discount not found")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _discountsApplication.Delete(id);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("Get/{id}")]
        [SwaggerOperation(
            Summary = "Get Discount",
            Description = "Get discount details by ID",
            OperationId = "GetDiscountById",
            Tags = new[] { "Discounts" })]
        [SwaggerResponse(200, "Discount details", typeof(Response<DiscountDto>))]
        [SwaggerResponse(404, "Discount not found")]
        [RequestTimeout("CustomPolicy")]
        public async Task<IActionResult> Get(int id)
        {
            //Sin TimeOut
            //var response = await _discountsApplication.Get(id);
            // Con TimeOut
            var response = await _discountsApplication.Get(id, HttpContext.RequestAborted);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("GetAll")]
        [SwaggerOperation(
            Summary = "Get All Discounts",
            Description = "Returns all discounts",
            OperationId = "GetAllDiscounts",
            Tags = new[] { "Discounts" })]
        [SwaggerResponse(200, "List of Discounts", typeof(Response<IEnumerable<DiscountDto>>))]
        [SwaggerResponse(404, "No discounts found")]
        public async Task<IActionResult> GetAll()
        {
            //Sin TimeOut
            //var response = await _discountsApplication.GetAll();
            // Con TimeOut
            var response = await _discountsApplication.GetAll(HttpContext.RequestAborted);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] int pageNumber, int pageSize)
        {
            var response = await _discountsApplication.GetAllWithPagination(pageNumber, pageSize);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response.Message);
        }
    }
}