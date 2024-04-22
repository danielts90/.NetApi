using FluentValidation.Results;
using MarketPlaceDomain.Entities;
using MarketPlaceDomain.Validators;

namespace MarketPlaceDomain.Dto
{
    public class ProductDto : DtoBase
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }

        public override ValidationResult Validate()
        {
            var validator = new ProductValidator();
            var validationResult = validator.Validate(this);
            return validationResult;
        }

        protected override EntityBase ToEntity() =>
            new Product()
            {
                Id = Id,
                Description = ProductDescription,
                Name = ProductName,
                Price = ProductPrice
            };
    }
}
