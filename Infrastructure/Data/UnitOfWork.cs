using Core.Interfaces;
using Core.Model;
using Infrastructure.Data.Repositories;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        // private GenericRepository<Product> productRepo;
        // private GenericRepository<Order> orderRepo;

        public UnitOfWork(DataContext context)
        {
            _context =  context;
        }

        // public GenericRepository<Product> ProductRepo
        // {
        //     get 
        //     { 
        //        if(this.productRepo == null){
        //            this.productRepo = new GenericRepository<Product>(_context);
        //        }

        //        return productRepo;
        //     }
        // }

        // public GenericRepository<Order> OrderRepo
        // {
        //     get 
        //     {
        //         if(this.orderRepo == null)
        //         {
        //             this.orderRepo = new GenericRepository<Order>(_context);
        //         }

        //         return orderRepo;
        //     }
        // }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            return new GenericRepository<T>(_context);
        }
    }
}