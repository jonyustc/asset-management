using Core.Model;

namespace Core.Interfaces
{
      public interface IUnitOfWork : IDisposable
    {
        Task<int> Complete();
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
    }
}