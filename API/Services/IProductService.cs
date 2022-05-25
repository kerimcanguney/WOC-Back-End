using API.Models;
using System.Collections.Generic;

namespace API.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product AddProduct(Product product);
    }
}
