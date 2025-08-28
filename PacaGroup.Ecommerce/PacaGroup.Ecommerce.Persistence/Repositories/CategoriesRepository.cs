using Dapper;
using PacaGroup.Ecommerce.Application.Interface.Persistence;
using PacaGroup.Ecommerce.Domain.Entities;
using PacaGroup.Ecommerce.Persistence.Contexts;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PacaGroup.Ecommerce.Persistence.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DapperContext _context;
        public CategoriesRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var query = "Select * From Categories";

            var categories = await connection.QueryAsync<Category>(query, commandType: CommandType.Text);
            return categories;
        }
    }
}
