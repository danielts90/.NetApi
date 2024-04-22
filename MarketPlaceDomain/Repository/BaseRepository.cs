using MarketPlaceDomain.Entities;
using MarketPlaceDomain.Interfaces;

namespace MarketPlaceDomain.Repository
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        protected List<T> entities;

        public BaseRepository()
        {
            entities = new List<T>();
        }

        public int Insert(T entity)
        {
            entity.Id = entities.Count() + 1;
            entity.CreatedAt = DateTime.Now;

            entities.Add(entity);
            return entity.Id;
        }

        public void Update(T entity)
        {
            var index = entities.FindIndex(e => e.Id == entity.Id);
            if (index != -1)
            {
                entities[index] = entity;
            }
            else
            {
                throw new ArgumentException("Entidade não encontrada.");
            }
        }

        public void Delete(int id)
        {
            var entityToRemove = entities.FirstOrDefault(e => e.Id == id);
            if (entityToRemove != null)
            {
                entities.Remove(entityToRemove);
            }
        }

        public T GetById(int id)
        {
            return entities.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities;
        }
    }
}
