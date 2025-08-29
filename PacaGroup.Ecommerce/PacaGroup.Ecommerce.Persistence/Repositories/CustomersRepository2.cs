using Dapper;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Domain.Entities;
using PacaGroup.Ecommerce.Persistence.Contexts;
using System.Data;

namespace PacaGroup.Ecommerce.Persistence.Repositories
{
    public class CustomersRepository2 : ICustomersRepository2
    {
        private readonly DapperContext _context;

        public CustomersRepository2(DapperContext context)
        {
            _context = context;
        }

        #region MyRegion

        public bool Insert(Customer entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string CustumerId)
        {
            throw new NotImplementedException();
        }

        public Customer GetById(string ustumerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(string ustumerId)
        {
            throw new NotImplementedException();
        }
        #endregion

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();
            var query = "CustomersList";

            var customers = await connection.QueryAsync<Customer>(query, commandType: CommandType.StoredProcedure);

            return customers;
        }

        public async Task<Customer?> GetAsync(string customerId)
        {
            using var connection = _context.CreateConnection();
            var query = "CustomersGetByID";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var customer = await connection.QuerySingleAsync<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);

            return customer;
        }

        public async Task<bool> InsertAsync(Customer customer)
        {
            using var connection = _context.CreateConnection();
            var query = "CustomersInsert";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerId);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            using var connection = _context.CreateConnection();
            var query = "CustomersUpdate";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customer.CustomerId);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            using var connection = _context.CreateConnection();
            var query = "CustomersDelete";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var result = await connection.ExecuteAsync(query, param: parameters, commandType: CommandType.StoredProcedure);

            return result > 0;
        }

        public async Task<IEnumerable<Customer>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            using var connection = _context.CreateConnection();
            var query = "CustomersListWithPagination";
            var parameters = new DynamicParameters();
            parameters.Add("PageNumber", pageNumber);
            parameters.Add("PageSize", pageSize);

            var customers = await connection.QueryAsync<Customer>(query, param: parameters, commandType: CommandType.StoredProcedure);
            return customers;
        }

        public async Task<int> CountAsync()
        {
            using var connection = _context.CreateConnection();
            var query = "Select Count(*) from Customers";

            var count = await connection.ExecuteScalarAsync<int>(query, commandType: CommandType.Text);
            return count;
        }

        public IEnumerable<Customer> GetAllWithPagination(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}
