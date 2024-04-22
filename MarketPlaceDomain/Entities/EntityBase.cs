using MarketPlaceDomain.Dto;

namespace MarketPlaceDomain.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }


        public static implicit operator DtoBase(EntityBase entity) => entity.ToDto();
        protected abstract DtoBase ToDto();
    }
}
