using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Entities;

namespace MarketPlaceBusiness.Interfaces
{
    public interface IBaseService<TDto, TEntity> where TDto : DtoBase where TEntity : EntityBase
    {
        int Insert(TDto dto);
        void Update(TDto dto);
        void Delete(int id);
        TDto GetById(int id);
        IEnumerable<TDto> GetAll();
    }
}
