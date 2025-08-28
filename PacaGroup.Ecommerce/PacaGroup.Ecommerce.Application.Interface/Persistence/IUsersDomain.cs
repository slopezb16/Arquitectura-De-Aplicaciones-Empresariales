using PacaGroup.Ecommerce.Domain.Entities;

namespace PacaGroup.Ecommerce.Application.Interface.Persistense
{
    public interface IUsersDomain
    {
        /// <summary>
        /// Metodo de autenticacion de usuarios
        /// Toma el nombre de usuario y la contraseña
        /// Deberia retornar el usuario autenticado
        User Authenticate(string userName, string password);
    }
}