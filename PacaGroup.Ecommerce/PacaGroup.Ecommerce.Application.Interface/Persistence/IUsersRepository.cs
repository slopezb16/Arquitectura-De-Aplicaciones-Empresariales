using PacaGroup.Ecommerce.Domain.Entities;

namespace PacaGroup.Ecommerce.Application.Interface.Persistense
{
    public interface IUsersRepository //: IGenericRepository<Users> // Solo se implementa si se necesitan los otros metodos
    {
        /// <summary>
        /// Metodo de autenticacion de usuarios
        /// Toma el nombre de usuario y la contraseña
        /// Deberia retornar el usuario autenticado
        Task<User> Authenticate(string userName, string password);
    }
}
