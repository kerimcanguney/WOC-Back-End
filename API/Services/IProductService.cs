using API.ProductModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product AddProduct(Product product);
        Product? GetProduct(string id);
        void updateProduct(string id, Product product);
        void deleteProduct(string id);
    }
}
