using PacaGroup.Ecommerce.Application.DTO;
using PacaGroup.Ecommerce.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Application.Interface.UseCases
{
    public interface IUsersApplication
    {
        /// <summary>
        /// Metodo de autenticacion de usuarios
        /// Toma el nombre de usuario y la contraseña
        /// Deberia retornar el usuario autenticado
        Response<UserDto> Authenticate(string userName, string password);
    }
}
