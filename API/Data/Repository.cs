using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly MaroDbContext _context;

        public Repository(MaroDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList() ?? Enumerable.Empty<T>();
        }

        public T Get(Guid id)
        {
            return _context.Set<T>().SingleOrDefault(c => c.Id == id);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
