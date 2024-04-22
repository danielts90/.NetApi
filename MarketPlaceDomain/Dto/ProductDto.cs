using FluentValidation.Results;
using MarketPlaceBusiness.Entities;
using MarketPlaceBusiness.Validators;

namespace MarketPlaceBusiness.Dto
{
    public class ProductDto : DtoBase
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? ProductPrice { get; set; }

        public override ValidationResult Validate()
        {
            var validator = new ProductValidator();
            return validator.Validate(this);
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
