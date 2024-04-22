using MarketPlaceDomain.Dto;
using MarketPlaceDomain.Entities;
using MarketPlaceDomain.Interfaces;

namespace MarketPlace.Controllers
{
    public class ProductController : BaseController<ProductDto, Product>   
    {
        public ProductController(IProductService productService) : base(productService)
        {
        }

        
    }

}

