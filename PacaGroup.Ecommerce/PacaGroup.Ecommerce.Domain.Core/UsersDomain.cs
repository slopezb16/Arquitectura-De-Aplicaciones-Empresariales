using PacaGroup.Ecommerce.Domain.Entity;
using PacaGroup.Ecommerce.Domain.Interface;
using PacaGroup.Ecommerce.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Domain.Core
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
        public Users Authenticate(string userName, string password)
        {
            //return _usersRepository.Authenticate(userName, password);
            return _unitOfWork.Users.Authenticate(userName, password);
        }
    }
}
