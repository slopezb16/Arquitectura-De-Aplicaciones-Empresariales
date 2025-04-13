using Microsoft.AspNetCore.Mvc;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PacaGroup.Ecommerce.Services.WebApi.Controllers
{
    // No necesitas especificar rutas en cada acción
    [Route("api/[controller]/[action]")]
    // APIs RESTful
    // Ejemplo [HttpGet("nombre")] en el metodo
    //[Route("api/[controller]")]
    [ApiController]
    //Ya que es una API y no usás Views, es mejor que heredes de ControllerBase en vez de Controller
    public class CustomersController : ControllerBase // Controller o ControllerBase
    {
        private readonly ICustomersApplication _customersApplication;

        // Constructor
        public CustomersController(ICustomersApplication customersApplication)
        {
            _customersApplication = customersApplication;
        }

        #region Métodos Sincrónicos

        [HttpPost]
        public IActionResult Insert([FromBody] CustomersDto customerDto)
        {
            if (customerDto == null)
                return BadRequest("El objeto no puede ser nulo");

            var response = _customersApplication.Insert(customerDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPut]
        public IActionResult Update([FromBody] CustomersDto customerDto)
        {
            if (customerDto == null)
                return BadRequest("El objeto no puede ser nulo");

            var response = _customersApplication.Update(customerDto);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpDelete("{customerId}")]
        public IActionResult Delete(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest("El customerId no puede ser nulo o vacio");

            var response = _customersApplication.Delete(customerId);
            if (response.IsSuccess)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("{customerId}")]
        public IActionResult GetById(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest("El customerId no puede ser nulo o vacio");

            var response = _customersApplication.GetById(customerId);
            if (response.IsSuccess && response.Data != null)
                return Ok(response);

            return NotFound(response);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _customersApplication.GetAll();
            if (response.IsSuccess && response.Data != null)
                return Ok(response);

            return NotFound(response);
        }
        #endregion

        #region Métodos Asíncronos

        [HttpPost("async")]
        public async Task<IActionResult> InsertAsync([FromBody] CustomersDto customerDto)
        {
            if (customerDto == null)
                return BadRequest("El objeto no puede ser nulo");

            var response = await _customersApplication.InsertAsync(customerDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpPut("async")]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomersDto customerDto)
        {
            if (customerDto == null)
                return BadRequest("El objeto no puede ser nulo");

            var response = await _customersApplication.UpdateAsync(customerDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("async/{customerId}")]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest("El customerId no puede ser nulo o vacio");

            var response = await _customersApplication.DeleteAsync(customerId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("async/{customerId}")]
        public async Task<IActionResult> GetByIdAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest("El customerId no puede ser nulo o vacio");

            var response = await _customersApplication.GetByIdAsync(customerId);
            return (response.IsSuccess && response.Data != null) ? Ok(response) : NotFound(response);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customersApplication.GetAllAsync();
            return (response.IsSuccess && response.Data != null) ? Ok(response) : NotFound(response);
        }

        #endregion
    }
}
