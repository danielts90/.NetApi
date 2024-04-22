using MarketPlaceDomain.Dto;
using MarketPlaceDomain.Entities;
using MarketPlaceDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceDomain.Services
{
    public class ProductService : BaseService<ProductDto, Product>, IProductService
    {
        public ProductService(IProductRepository repository) : base(repository)
        {
        }
    }
}
