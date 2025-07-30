using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetQuerable();

        IQueryable<T> GetQuerable(Expression<Func<T, bool>> criteria);

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);

        Task ExecuteStoredProcedureAsync(string storedProcedure, params object[] parameters);

        Task<IEnumerable<T>> ExecuteStoredProcedureQueryAsync(string storedProcedure, params object[] parameters);
    }
}