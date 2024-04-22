using MarketPlaceBusiness.Dto;
using MarketPlaceBusiness.Entities;
using MarketPlaceBusiness.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceBusiness.Services
{
    public class ProductService : BaseService<ProductDto, Product>, IProductService
    {
        public ProductService(IProductRepository repository) : base(repository)
        {
        }
    }
}
