using App.Core.Model;
using App.DataAccess.Abstractions;

namespace App.DataAccess.Impl
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly MaroDbContext _context;

        public Repository(MaroDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Set<T>().Add(entity);
        }

        public bool Exists(int entityId)
        {
            return _context.Set<T>().Any(c => c.Id == entityId);

        }

        public T Get(int id)
        {
            return _context.Set<T>().SingleOrDefault(c => c.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable() ?? Enumerable.Empty<T>().AsQueryable();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}