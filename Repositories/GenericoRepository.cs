using _0411.Contracts;
using _0411.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _0411.Repositories
{
    public class GenericoRepository<T>(DataContext dataContext) : IGenericoRepository<T> where T : class
    {
        protected readonly DataContext _entityFramework = dataContext;

        public async Task<T> Create(T entity)
        {
            await _entityFramework.Set<T>().AddAsync(entity);

            return entity;
        }

        public T Delete(T entity)
        {
            _entityFramework.Set<T>().Remove(entity);

            return entity;
        }

        public async Task<T?> Get(Expression<Func<T, bool>> predicate)
        {
            return await _entityFramework.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entityFramework.Set<T>().AsNoTracking().ToListAsync();
        }

        public T Update(T entity)
        {
            _entityFramework.Set<T>().Update(entity);

            return entity;
        }        
    }
}
