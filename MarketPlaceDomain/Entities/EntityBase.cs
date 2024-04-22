using MarketPlaceBusiness.Dto;

namespace MarketPlaceBusiness.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }


        public static implicit operator DtoBase(EntityBase entity) => entity.ToDto();
        protected abstract DtoBase ToDto();
    }
}
