using App.Core.Model;
using App.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

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

        public void Update(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Entry(entity).State = EntityState.Modified;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}