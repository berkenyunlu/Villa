using MongoDB.Bson;
using System.Linq.Expressions;

namespace Villa.DataAccess.Abstract;

public interface IGenericDal<T> where T : class
{
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(ObjectId id);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
    Task<T> GetByIdAsync(ObjectId id);
    Task<int> CountAsync();
}
