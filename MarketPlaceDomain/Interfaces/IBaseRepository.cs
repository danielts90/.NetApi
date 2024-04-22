using MarketPlaceDomain.Entities;

namespace MarketPlaceDomain.Interfaces
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        int Insert(T entity);
        void Update(T entity);
        void Delete(int id);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
