using System.Linq.Expressions;
using Core.Model;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<IReadOnlyList<T>> FindListAllAsync(Expression<Func<T,bool>> expression);
        Task<T> GetByIdAsync(int id);
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveChangesAsync();
        IQueryable<T> GetQueryAble();
        Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression);

    }
}