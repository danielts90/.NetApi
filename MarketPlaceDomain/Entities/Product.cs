using MarketPlaceBusiness.Dto;

namespace MarketPlaceBusiness.Entities
{
    public class Product : EntityBase
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }

        protected override DtoBase ToDto()
        {
            return new ProductDto
            {
                Id = Id,
                ProductName = Name,
                ProductDescription = Description,
                ProductPrice = Price
            };
        }
    }
}
