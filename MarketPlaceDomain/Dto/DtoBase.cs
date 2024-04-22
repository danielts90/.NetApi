using FluentValidation.Results;
using MarketPlaceBusiness.Entities;

namespace MarketPlaceBusiness.Dto
{
    public abstract class DtoBase
    {
        public int Id { get; set; }

        public static implicit operator EntityBase(DtoBase dto) => dto.ToEntity();

        protected abstract EntityBase ToEntity();

        public abstract ValidationResult Validate();
    }
}
