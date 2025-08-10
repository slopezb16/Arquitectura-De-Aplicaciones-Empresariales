using AutoMapper;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface;
using PacaGroup.Ecommerce.Domain.Entity;
using PacaGroup.Ecommerce.Domain.Interface;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.Main
{
    public class CustomersApplication2 : ICustomersApplication2
    {
        private readonly ICustomersDomain2 _CustomersDomain2;
        private readonly IMapper _mapper;

        public CustomersApplication2(ICustomersDomain2 CustomersDomain, IMapper mapper)
        {
            _CustomersDomain2 = CustomersDomain;
            _mapper = mapper;
        }

        public async Task<Response<bool>> InsertAsync(CustomersDto2 CustomerssDto)
        {
            var response = new Response<bool>();
            try
            {
                var Customers = _mapper.Map<Customers>(CustomerssDto);
                response.Data = await _CustomersDomain2.InsertAsync(Customers);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<bool>> UpdateAsync(CustomersDto2 CustomerssDto)
        {
            var response = new Response<bool>();
            try
            {
                var Customers = _mapper.Map<Customers>(CustomerssDto);
                response.Data = await _CustomersDomain2.UpdateAsync(Customers);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(string CustomersId)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _CustomersDomain2.DeleteAsync(CustomersId);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<CustomersDto2>> GetAsync(string CustomersId)
        {
            var response = new Response<CustomersDto2>();
            try
            {
                var Customers = await _CustomersDomain2.GetAsync(CustomersId);
                response.Data = _mapper.Map<CustomersDto2>(Customers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<IEnumerable<CustomersDto2>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomersDto2>>();
            try
            {
                var Customerss = await _CustomersDomain2.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CustomersDto2>>(Customerss);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }      
    }
}
