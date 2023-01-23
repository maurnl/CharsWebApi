using App.Core.Model;

namespace App.DataAccess.Abstractions
{
    public interface IRepository<T> where T : EntityBase
    {
        IQueryable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        bool Exists(int entityId);
        bool SaveChanges();
    }
}
