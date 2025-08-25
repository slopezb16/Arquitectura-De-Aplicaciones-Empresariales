using AutoMapper;
using FluentValidation;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Application.Interface.UseCases;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.UseCases.Users
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly UsersDtoValidator _usersDtoValidator;
        private readonly IValidator<UserDto> _usersDtoValidator;

        public UsersApplication(IMapper mapper, IUnitOfWork unitOfWork, IValidator<UserDto> usersDtoValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _usersDtoValidator = usersDtoValidator;
        }

        public Response<UserDto> Authenticate(string userName, string password)
        {
            var response = new Response<UserDto>();
            var validation = _usersDtoValidator.Validate(new UserDto() { UserName = userName, Password = password });

            if (!validation.IsValid)
            {
                response.Message = "Errores de Validación";
                response.Errors = validation.Errors;
                return response;
            }
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                response.Message = "Parámetros no pueden ser vacios.";
                return response;
            }
            try
            {
                var user = _unitOfWork.Users.Authenticate(userName, password);
                response.Data = _mapper.Map<UserDto>(user);
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
