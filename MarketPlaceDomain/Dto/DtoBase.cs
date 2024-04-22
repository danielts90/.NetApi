using FluentValidation.Results;
using MarketPlaceDomain.Entities;

namespace MarketPlaceDomain.Dto
{
    public abstract class DtoBase
    {
        public int Id { get; set; }

        public static implicit operator EntityBase(DtoBase dto) => dto.ToEntity();

        protected abstract EntityBase ToEntity();

        public abstract ValidationResult Validate();
    }
}
