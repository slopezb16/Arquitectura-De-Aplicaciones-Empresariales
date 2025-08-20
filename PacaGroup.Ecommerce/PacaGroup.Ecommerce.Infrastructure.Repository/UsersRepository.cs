using Dapper;
using PacaGroup.Ecommerce.Domain.Entity;
using PacaGroup.Ecommerce.Infrastructure.Data;
using PacaGroup.Ecommerce.Infrastructure.Interface;
using System.Data;

namespace PacaGroup.Ecommerce.Infrastructure.Repository
{
    public class UsersRepository : IUsersRepository
    {
        //private readonly IConnectionFactory _connectionFactory;
        private readonly DapperContext _context;
        // Constructor
        //public UsersRepository(IConnectionFactory connectionFactory)
        public UsersRepository(DapperContext context)
        {
            //_connectionFactory = connectionFactory;
            _context = context;
        }
        // Implementación del método Authenticate
        public Users Authenticate(string userName, string password)
        {
            //using (var connection = _connectionFactory.GetConnection)
            using (var connection = _context.CreateConnection())
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("UserName", userName);
                parameters.Add("Password", password);

                var user = connection.QuerySingle<Users>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
    }
}
