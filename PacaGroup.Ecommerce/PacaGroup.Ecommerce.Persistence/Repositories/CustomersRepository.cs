using Dapper;
using PacaGroup.Ecommerce.Domain.Entity;
using PacaGroup.Ecommerce.Application.Interface.Persistense;
using PacaGroup.Ecommerce.Transversal.Common;
using System.Data;

namespace PacaGroup.Ecommerce.Persistence.Repositories
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public CustomersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        #region Metodos Sincronos
        public bool Insert(Customer custumer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                //using (var transaction = connection.BeginTransaction())
                //{
                //    try
                //    {
                //        // Insert logic here
                //        transaction.Commit();
                //        return true;
                //    }
                //    catch (Exception)
                //    {
                //        transaction.Rollback();
                //        return false;
                //    }
                //}
                var query = "CustomersInsert";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", custumer.CustomerId);
                parameters.Add("CompanyName", custumer.CompanyName);
                parameters.Add("ContactName", custumer.ContactName);
                parameters.Add("ContactTitle", custumer.ContactTitle);
                parameters.Add("Address", custumer.Address);
                parameters.Add("City", custumer.City);
                parameters.Add("Region", custumer.Region);
                parameters.Add("PostalCode", custumer.PostalCode);
                parameters.Add("Country", custumer.Country);
                parameters.Add("Phone", custumer.Phone);
                parameters.Add("Fax", custumer.Fax);
                //parameters.Add("Result", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = connection.Execute(query, param: parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }
        public bool Update(Customer customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customer.CustomerId);
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

                var result = connection.Execute(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public bool Delete(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersDelete";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customerId);

                var result = connection.Execute(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public Customer GetById(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customerId);

                return connection.QuerySingleOrDefault<Customer>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersList";
                return connection.Query<Customer>(query, commandType: CommandType.StoredProcedure);
            }
        }
        #endregion

        #region Metodos Asincronos
        public async Task<bool> InsertAsync(Customer customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersInsert";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customer.CustomerId);
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

                var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customer.CustomerId);
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

                var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersDelete";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customerId);

                var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return result > 0;
            }
        }

        public async Task<Customer> GetByIdAsync(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customerId);

                return await connection.QuerySingleOrDefaultAsync<Customer>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersList";
                return await connection.QueryAsync<Customer>(query, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion
    }
}
