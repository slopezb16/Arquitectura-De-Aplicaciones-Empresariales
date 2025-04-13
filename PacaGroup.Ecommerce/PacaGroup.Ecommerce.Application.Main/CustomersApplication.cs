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
        public CustomersApplication(IMapper mapper, ICustomersDomain customersDomain)
        {
            _mapper = mapper;
            _customersDomain = customersDomain;
        }

        #region Métodos Síncronos

        public Response<bool> Insert(CustomersDto customerDto)
        {
            var response = new Response<bool>();
            try
            {
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = _customersDomain.Insert(customer);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente registrado exitosamente." : "No se pudo registrar el cliente.";
            }
            catch (Exception ex)
            {
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
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = _customersDomain.Update(customer);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente actualizado correctamente." : "No se pudo actualizar el cliente.";
            }
            catch (Exception ex)
            {
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
                response.Data = _customersDomain.Delete(customerId);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente eliminado correctamente." : "No se pudo eliminar el cliente.";
            }
            catch (Exception ex)
            {
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
                var customer = _customersDomain.GetById(customerId);
                if (customer != null)
                {
                    response.Data = _mapper.Map<CustomersDto>(customer);
                    response.IsSuccess = true;
                    response.Message = "Cliente encontrado.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Cliente no encontrado.";
                }
            }
            catch (Exception ex)
            {
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
                var customers = _customersDomain.GetAll();
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Clientes listados correctamente.";
                }
                else
                {
                    response.Data = new List<CustomersDto>();
                    response.IsSuccess = false; // opcionalmente podrías dejarlo en true
                    response.Message = "No se encontraron clientes registrados.";
                }
            }
            catch (Exception ex)
            {
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
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = await _customersDomain.InsertAsync(customer);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente registrado exitosamente." : "No se pudo registrar el cliente.";
            }
            catch (Exception ex)
            {
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
                var customer = _mapper.Map<Customers>(customerDto);
                response.Data = await _customersDomain.UpdateAsync(customer);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente actualizado correctamente." : "No se pudo actualizar el cliente.";
            }
            catch (Exception ex)
            {
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
                response.Data = await _customersDomain.DeleteAsync(customerId);
                response.IsSuccess = response.Data;
                response.Message = response.Data ? "Cliente eliminado correctamente." : "No se pudo eliminar el cliente.";
            }
            catch (Exception ex)
            {
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
                var customer = await _customersDomain.GetByIdAsync(customerId);
                if (customer != null)
                {
                    response.Data = _mapper.Map<CustomersDto>(customer);
                    response.IsSuccess = true;
                    response.Message = "Cliente encontrado.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Cliente no encontrado.";
                }
            }
            catch (Exception ex)
            {
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
                var customers = await _customersDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Clientes listados correctamente.";
                }
                else
                {
                    response.Data = new List<CustomersDto>();
                    response.IsSuccess = false; // opcionalmente podrías dejarlo en true
                    response.Message = "No se encontraron clientes registrados.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Error al listar clientes: {ex.Message}";
            }

            return response;
        }
        #endregion
    }
}
