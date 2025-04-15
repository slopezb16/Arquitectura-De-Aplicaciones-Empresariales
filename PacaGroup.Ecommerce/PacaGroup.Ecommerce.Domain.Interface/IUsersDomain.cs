using PacaGroup.Ecommerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Domain.Interface
{
    public interface IUsersDomain
    {
        /// <summary>
        /// Metodo de autenticacion de usuarios
        /// Toma el nombre de usuario y la contraseña
        /// Deberia retornar el usuario autenticado
        Users Authenticate(string userName, string password);
    }
}
