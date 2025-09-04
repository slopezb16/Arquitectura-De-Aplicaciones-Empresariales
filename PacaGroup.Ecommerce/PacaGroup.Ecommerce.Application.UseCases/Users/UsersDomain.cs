using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Domain.Entities;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Application.UseCases
{
    public class UsersDomain : IUsersDomain
    {
        //private readonly IUsersRepository _usersRepository;
        private readonly IUnitOfWork _unitOfWork;
        // Constructor
        //public UsersDomain(IUsersRepository usersRepository)
        public UsersDomain(IUnitOfWork unitOfWork)
        {
            //_usersRepository = usersRepository;
            _unitOfWork = unitOfWork;
        }
        // Implementación del método Authenticate
        public async Task<User> Authenticate(string userName, string password)
        {
            //return _usersRepository.Authenticate(userName, password);
            return await _unitOfWork.Users.Authenticate(userName, password);
        }
    }
}