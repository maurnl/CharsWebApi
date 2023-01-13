using API.Model;

namespace API.Data
{
    public interface IRepository<T> where T : EntityBase
    {
        IEnumerable<T> GetAll();
        T Get(Guid id);
        void Add(T entity);
        void SaveChanges();
    }
}
