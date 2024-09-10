using System.Linq.Expressions;

namespace _0411.Contracts
{
    public interface IGenericoRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> Get(Expression<Func<T, bool>> predicate);
        Task<T> Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
