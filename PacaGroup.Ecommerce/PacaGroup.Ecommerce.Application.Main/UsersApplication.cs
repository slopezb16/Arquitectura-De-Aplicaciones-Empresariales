using AutoMapper;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface;
using PacaGroup.Ecommerce.Domain.Interface;
using PacaGroup.Ecommerce.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Application.Main
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IMapper _mapper;
        private readonly IUsersDomain _usersDomain;

        public UsersApplication(IMapper mapper, IUsersDomain usersDomain)
        {
            _mapper = mapper;
            _usersDomain = usersDomain;
        }

        public Response<UsersDto> Authenticate(string userName, string password)
        {
            var response = new Response<UsersDto>();
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                response.Message = "Parámetros no pueden ser vacios.";
                return response;
            }
            try
            {
                var user = _usersDomain.Authenticate(userName, password);
                response.Data = _mapper.Map<UsersDto>(user);
                response.IsSuccess = true;
                response.Message = "Autenticación Exitosa!!!";
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
    }
}
