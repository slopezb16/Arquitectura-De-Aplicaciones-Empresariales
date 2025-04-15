using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PacaGroup.Ecommerce.Services.WebApi.Controllers
{
    /// <summary>
    /// Controlador para gestionar operaciones CRUD de clientes.
    /// </summary>
    // No necesitas especificar rutas en cada acción
    [Route("api/[controller]/[action]")]
    // APIs RESTful
    // Ejemplo [HttpGet("nombre")] en el metodo
    //[Route("api/[controller]")]
    [ApiController]
    //Ya que es una API y no usás Views, es mejor que heredes de ControllerBase en vez de Controller
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomersController : ControllerBase // Controller o ControllerBase
    {
        private readonly ICustomersApplication _customersApplication;

        // Constructor
        public CustomersController(ICustomersApplication customersApplication)
        {
            _customersApplication = customersApplication;
        }

        #region Métodos Sincrónicos

        /// <summary>
        /// Inserta un nuevo cliente.
        /// </summary>
        /// <param name="customerDto">Datos del cliente a insertar.</param>
        /// <returns>Resultado de la operación.</returns>
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

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="customerDto">Datos del cliente a actualizar.</param>
        /// <returns>Resultado de la operación.</returns>
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

        /// <summary>
        /// Elimina un cliente por su identificador.
        /// </summary>
        /// <param name="customerId">ID del cliente.</param>
        /// <returns>Resultado de la operación.</returns>
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

        /// <summary>
        /// Obtiene un cliente por su identificador.
        /// </summary>
        /// <param name="customerId">ID del cliente.</param>
        /// <returns>Cliente encontrado o mensaje de error.</returns>
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

        /// <summary>
        /// Obtiene todos los clientes.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        //[AllowAnonymous]
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

        /// <summary>
        /// Inserta un nuevo cliente (versión asíncrona).
        /// </summary>
        /// <param name="customerDto">Datos del cliente a insertar.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("async")]
        public async Task<IActionResult> InsertAsync([FromBody] CustomersDto customerDto)
        {
            if (customerDto == null)
                return BadRequest("El objeto no puede ser nulo");

            var response = await _customersApplication.InsertAsync(customerDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Actualiza un cliente existente (versión asíncrona).
        /// </summary>
        /// <param name="customerDto">Datos del cliente a actualizar.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPut("async")]
        public async Task<IActionResult> UpdateAsync([FromBody] CustomersDto customerDto)
        {
            if (customerDto == null)
                return BadRequest("El objeto no puede ser nulo");

            var response = await _customersApplication.UpdateAsync(customerDto);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Elimina un cliente por su ID (versión asíncrona).
        /// </summary>
        /// <param name="customerId">ID del cliente.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpDelete("async/{customerId}")]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest("El customerId no puede ser nulo o vacio");

            var response = await _customersApplication.DeleteAsync(customerId);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Obtiene un cliente por su ID (versión asíncrona).
        /// </summary>
        /// <param name="customerId">ID del cliente.</param>
        /// <returns>Cliente encontrado o mensaje de error.</returns>
        [HttpGet("async/{customerId}")]
        public async Task<IActionResult> GetByIdAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
                return BadRequest("El customerId no puede ser nulo o vacio");

            var response = await _customersApplication.GetByIdAsync(customerId);
            return (response.IsSuccess && response.Data != null) ? Ok(response) : NotFound(response);
        }

        /// <summary>
        /// Obtiene todos los clientes (versión asíncrona).
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        [HttpGet("async")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customersApplication.GetAllAsync();
            return (response.IsSuccess && response.Data != null) ? Ok(response) : NotFound(response);
        }

        #endregion
    }
}
