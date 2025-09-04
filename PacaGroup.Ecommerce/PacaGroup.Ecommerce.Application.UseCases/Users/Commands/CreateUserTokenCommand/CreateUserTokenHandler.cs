using AutoMapper;
using MediatR;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand
{
    public class CreateUserTokenHandler : IRequestHandler<CreateUserTokenCommand, Response<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserTokenHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<UserDto>> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<UserDto>();
            var user = await _unitOfWork.Users.Authenticate(request.userName, request.password);
            //var user = _unitOfWork.Users.Authenticate(request.userName, request.password);
            if (user is null)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe";
                return response;
            }

            response.Data = _mapper.Map<UserDto>(user);
            response.IsSuccess = true;
            response.Message = "Autenticación Exitosa!!!";

            return response;
        }
    }
}
