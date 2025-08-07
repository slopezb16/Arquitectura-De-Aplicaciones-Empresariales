using Dapper;
using PacaGroup.Ecommerce.Domain.Entity;
using PacaGroup.Ecommerce.Infrastructure.Data;
using PacaGroup.Ecommerce.Infrastructure.Interface;
using System.Data;

namespace PacaGroup.Ecommerce.Infrastructure.Repository
{
    public class CustomersRepository2 : ICustomersRepository2
    {
        private readonly DapperContext _context;

        public CustomersRepository2(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();
            var query = "CustomersList";

            var customers = await connection.QueryAsync<Customers>(query, commandType: CommandType.StoredProcedure);

            return customers;
        }

        public async Task<Customers?> GetAsync(string customerId)
        {
            using var connection = _context.CreateConnection();
            var query = "CustomersGetByID";
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var customer = await connection.QuerySingleAsync<Customers>(query, param: parameters, commandType: CommandType.StoredProcedure);

            return customer;
        }

        public async Task<bool> InsertAsync(Customers customer)
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

        public async Task<bool> UpdateAsync(Customers customer)
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
    }
}
