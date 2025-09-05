using Dapper;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using System.Data;
using PacaGroup.Ecommerce.Persistence.Contexts;
using PacaGroup.Ecommerce.Domain.Entities;

namespace PacaGroup.Ecommerce.Persistence.Repositories
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
        //public User Authenticate(string userName, string password)
        public async Task<User> Authenticate(string userName, string password)
        {
            //using (var connection = _connectionFactory.GetConnection)
            using (var connection = _context.CreateConnection())
            {
                var query = "UsersGetByUserAndPassword";
                var parameters = new DynamicParameters();
                parameters.Add("UserName", userName);
                parameters.Add("Password", password);

                //var user = connection.QuerySingle<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, param: parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
    }
}
