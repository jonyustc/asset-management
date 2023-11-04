using System.Linq.Expressions;
using Core.Interfaces;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        

       

        

        

        public async Task<T> Add(T entity)
        {
            await _context.AddAsync<T>(entity);

            return entity;
        }

        public void Update(T entity)
        {
            //_context.Attach<T>(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
           _context.Remove<T>(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> FindListAllAsync(Expression<Func<T, bool>> expression)
        {
            var result = _context.Set<T>().Where(expression);

            return await result.ToListAsync();
        }

        public async Task<T> GetByExpressionAsync(Expression<Func<T,bool>> expression)
        {
            return await _context.Set<T>().Where(expression).FirstOrDefaultAsync();
        }

        public IQueryable<T> GetQueryAble()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}