using MediatR;
using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;

namespace PacaGroup.Ecommerce.Application.UseCases.Users.Commands.CreateUserTokenCommand
{
    public sealed record CreateUserTokenCommand: IRequest<Response<UserDto>>
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
