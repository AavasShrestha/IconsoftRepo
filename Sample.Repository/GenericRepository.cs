using CBS.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CBS.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly TenantDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(TenantDbContext context)
        {
            _context = context ?? throw new ArgumentNullException("Context was not supplied"); ;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetQuerable()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> GetQuerable(Expression<Func<T, bool>> criteria)
        {
            return _dbSet.Where(criteria).AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity was not supplied");
            }

            _dbSet.Add(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity was not supplied");
            }

            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity was not supplied");
            }

            _dbSet.Remove(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task ExecuteStoredProcedureAsync(string storedProcedure, params object[] parameters)
        {
            await _context.Database.ExecuteSqlRawAsync(storedProcedure, parameters);
        }

        public async Task<IEnumerable<T>> ExecuteStoredProcedureQueryAsync(string storedProcedure, params object[] parameters)
        {
            return await _context.Set<T>().FromSqlRaw(storedProcedure, parameters).ToListAsync();
        }
    }
}