using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Entities;
using MarketPlaceBusiness.Interfaces;

namespace MarketPlace.Controllers
{
    public class ProductController : BaseController<ProductDto, Product>   
    {
        public ProductController(IProductService productService) : base(productService)
        {
        }

        
    }

}

