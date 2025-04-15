using Dapper;
using PacaGroup.Ecommerce.Domain.Entity;
using PacaGroup.Ecommerce.Infrastructure.Interface;
using PacaGroup.Ecommerce.Transversal.Common;
using System.Data;

namespace PacaGroup.Ecommerce.Infrastructure.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public CustomersRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        #region Metodos Sincronos
        public bool Insert(Customers custumer)
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
        public bool Update(Customers customer)
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

        public Customers GetById(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customerId);

                return connection.QuerySingleOrDefault<Customers>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<Customers> GetAll()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersList";
                return connection.Query<Customers>(query, commandType: CommandType.StoredProcedure);
            }
        }
        #endregion

        #region Metodos Asincronos
        public async Task<bool> InsertAsync(Customers customer)
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

        public async Task<bool> UpdateAsync(Customers customer)
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

        public async Task<Customers> GetByIdAsync(string customerId)
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersGetByID";
                var parameters = new DynamicParameters();
                parameters.Add("CustomerId", customerId);

                return await connection.QuerySingleOrDefaultAsync<Customers>(query, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Customers>> GetAllAsync()
        {
            using (var connection = _connectionFactory.GetConnection)
            {
                var query = "CustomersList";
                return await connection.QueryAsync<Customers>(query, commandType: CommandType.StoredProcedure);
            }
        }

        #endregion
    }
}
