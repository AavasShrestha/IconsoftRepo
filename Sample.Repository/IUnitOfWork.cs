using CBS.Data.RoutingDB;
using CBS.Data.TenantDB;
using Microsoft.Data.SqlClient;
using Sample.Data.TenantDB;
using System.Data.Common;

namespace CBS.Repository
{
    public interface IUnitOfWork
    {
        IRepository<Tenant> TenantRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Client> ClientRepository { get; }

        bool Commit();

        List<T> ExecuteStoredProcedure<T>(string storedProcedure, Func<DbDataReader, T> map, params SqlParameter[] parameters);

        List<List<T>> ExecuteStoredProcedureMultipleResults<T>(string storedProcedure, Func<DbDataReader, T> map, params SqlParameter[] parameters);

        List<List<object>> ExecuteStoredProcedureMultipleResults(string storedProcedure, params SqlParameter[] parameters);
    }
}