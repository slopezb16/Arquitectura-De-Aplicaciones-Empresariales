using System;
using AutoMapper;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface;
using PacaGroup.Ecommerce.Domain.Entity;
using PacaGroup.Ecommerce.Domain.Interface;
using PacaGroup.Ecommerce.Transversal.Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PacaGroup.Ecommerce.Application.Main
{
    public class CustomersApplication : ICustomersApplication
    {
        private readonly IMapper _mapper;
        private readonly ICustomersDomain _customersDomain;
        private readonly IAppLogger<CustomersApplication> _logger;
        public CustomersApplication(IMapper mapper, ICustomersDomain customersDomain, IAppLogger<CustomersApplication> logger)
        {
            _mapper = mapper;
            _customersDomain = customersDomain;
            _logger = logger;
        }

        #region Métodos Síncronos

        // Ejemplo de uso del logger en Insert
        public Response<bool> Insert(CustomersDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                _logger.LogInformation("Iniciando inserción de cliente.");

                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = _customersDomain.Insert(customer);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente registrado exitosamente." : "No se pudo registrar el cliente.";

                _logger.LogInformation("Resultado de inserción: {0}", response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al insertar cliente: {0}", ex.Message);
                response.IsSuccess = false;
                response.Message = $"Error al insertar cliente: {ex.Message}";
            }

            return response;
        }

        public Response<bool> Update(CustomersDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                _logger.LogInformation("Iniciando actualización de cliente.");

                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = _customersDomain.Update(customer);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente actualizado correctamente." : "No se pudo actualizar el cliente.";

                _logger.LogInformation("Resultado de actualización: {0}", response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al actualizar cliente: {0}", ex.Message);
                response.IsSuccess = false;
                response.Message = $"Error al actualizar cliente: {ex.Message}";
            }

            return response;
        }

        public Response<bool> Delete(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                _logger.LogInformation("Iniciando eliminación del cliente con ID: {0}", customerId);

                response.Data = _customersDomain.Delete(customerId);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente eliminado correctamente." : "No se pudo eliminar el cliente.";

                _logger.LogInformation("Resultado de eliminación: {0}", response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al eliminar cliente: {0}", ex.Message);
                response.IsSuccess = false;
                response.Message = $"Error al eliminar cliente: {ex.Message}";
            }

            return response;
        }

        public Response<CustomersDto> GetById(string customerId)
        {
            var response = new Response<CustomersDto>();
            try
            {
                _logger.LogInformation("Buscando cliente con ID: {0}", customerId);

                var customer = _customersDomain.GetById(customerId);
                if (customer != null)
                {
                    response.Data = _mapper.Map<CustomersDto>(customer);
                    response.IsSuccess = true;
                    response.Message = "Cliente encontrado.";

                    _logger.LogInformation("Cliente encontrado.");
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Cliente no encontrado.";

                    _logger.LogWarning("Cliente con ID {0} no encontrado.", customerId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al obtener cliente: {0}", ex.Message);
                response.IsSuccess = false;
                response.Message = $"Error al obtener cliente: {ex.Message}";
            }

            return response;
        }

        public Response<IEnumerable<CustomersDto>> GetAll()
        {
            var response = new Response<IEnumerable<CustomersDto>>();
            try
            {
                _logger.LogInformation("Iniciando listado de todos los clientes.");

                var customers = _customersDomain.GetAll();
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Clientes listados correctamente.";

                    _logger.LogInformation("Clientes listados exitosamente.");
                }
                else
                {
                    response.Data = new List<CustomersDto>();
                    response.IsSuccess = false;
                    response.Message = "No se encontraron clientes registrados.";

                    _logger.LogWarning("No hay clientes en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al listar clientes: {0}", ex.Message);
                response.IsSuccess = false;
                response.Message = $"Error al listar clientes: {ex.Message}";
            }

            return response;
        }
        #endregion

        #region Métodos Asíncronos

        public async Task<Response<bool>> InsertAsync(CustomersDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                _logger.LogInformation("Iniciando inserción async de cliente.");

                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = await _customersDomain.InsertAsync(customer);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente registrado exitosamente." : "No se pudo registrar el cliente.";

                _logger.LogInformation("Resultado de inserción async: {0}", response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error async al insertar cliente: {0}", ex.Message);
                response.IsSuccess = false;
                response.Message = $"Error al insertar cliente: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<bool>> UpdateAsync(CustomersDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                _logger.LogInformation("Iniciando actualización async de cliente.");

                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = await _customersDomain.UpdateAsync(customer);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente actualizado correctamente." : "No se pudo actualizar el cliente.";

                _logger.LogInformation("Resultado de actualización async: {0}", response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error async al actualizar cliente: {0}", ex.Message);
                response.IsSuccess = false;
                response.Message = $"Error al actualizar cliente: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();
            try
            {
                _logger.LogInformation("Iniciando eliminación async del cliente con ID: {0}", customerId);

                response.Data = await _customersDomain.DeleteAsync(customerId);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente eliminado correctamente." : "No se pudo eliminar el cliente.";

                _logger.LogInformation("Resultado de eliminación async: {0}", response.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error async al eliminar cliente: {0}", ex.Message);
                response.IsSuccess = false;
                response.Message = $"Error al eliminar cliente: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<CustomersDto>> GetByIdAsync(string customerId)
        {
            var response = new Response<CustomersDto>();
            try
            {
                _logger.LogInformation("Buscando async cliente con ID: {0}", customerId);

                var customer = await _customersDomain.GetByIdAsync(customerId);
                if (customer != null)
                {
                    response.Data = _mapper.Map<CustomersDto>(customer);
                    response.IsSuccess = true;
                    response.Message = "Cliente encontrado.";

                    _logger.LogInformation("Cliente encontrado async.");
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Cliente no encontrado.";

                    _logger.LogWarning("Cliente con ID {0} no encontrado async.", customerId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error async al obtener cliente: {0}", ex.Message);
                response.IsSuccess = false;
                response.Message = $"Error al obtener cliente: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<IEnumerable<CustomersDto>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomersDto>>();
            try
            {
                _logger.LogInformation("Iniciando listado async de clientes.");

                var customers = await _customersDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Clientes listados correctamente.";

                    _logger.LogInformation("Clientes listados correctamente async.");
                }
                else
                {
                    response.Data = new List<CustomersDto>();
                    response.IsSuccess = false;
                    response.Message = "No se encontraron clientes registrados.";

                    _logger.LogWarning("No se encontraron clientes async.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error async al listar clientes: {0}", ex.Message);
                response.IsSuccess = false;
                response.Message = $"Error al listar clientes: {ex.Message}";
            }

            return response;
        }
        #endregion
    }
}
