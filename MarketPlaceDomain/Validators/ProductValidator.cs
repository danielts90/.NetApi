using FluentValidation;
using MarketPlaceBusiness.Dto;

namespace MarketPlaceBusiness.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductName)
              .NotEmpty().WithMessage("The Product Name is required");
            RuleFor(x => x.ProductDescription)
                .NotEmpty().WithMessage("The product description is required.")
                .NotEqual("string").WithMessage("The value \"string\" is invalid.")
                .MinimumLength(5).WithMessage("The product description needs to have 5 or more characters");
            RuleFor(x => x.ProductPrice)
                .NotNull()
                .NotEmpty().WithMessage("The Product Price is required")
                .GreaterThan(0).WithMessage("The prodct value needs to be greater than 0");                
        }
    }
}
