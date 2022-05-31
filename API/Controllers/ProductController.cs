using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Services;
using API.ProductModels;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts().ToArray());
        }
        [HttpPost, Route("/post")]
        public IActionResult add(string name, string type, string category)
        {
            if (Enum.IsDefined(typeof(ProductEnums.ProductTypeEnum), type) == false)  return null; // type bestaat niet
            if (Enum.IsDefined(typeof(ProductEnums.ProductCategoryEnum), category) == false)  return null; // category bestaat niet

            ProductEnums.ProductTypeEnum ProductType = (ProductEnums.ProductTypeEnum)Enum.Parse(typeof(ProductEnums.ProductTypeEnum), type);
            ProductEnums.ProductCategoryEnum ProductCategory = (ProductEnums.ProductCategoryEnum)Enum.Parse(typeof(ProductEnums.ProductCategoryEnum), category);

            Product product = new Product(name, ProductType, ProductCategory);
            
            return Ok(_productService.AddProduct(product));
        }

        [HttpGet, Route("/getbyid")]
        public IActionResult getById(string id)
        {
            return Ok(_productService.GetProduct(id));
        }

        [HttpPut]
        public IActionResult updateProduct(string id, Product product)
        {
            product.Id = id;
            _productService.updateProduct(id, product);
            return Ok("Done");
        }
        [HttpDelete]
        public IActionResult deleteProduct(string id)
        {
            _productService.deleteProduct(id);
            return Ok("Done");
        }
    }
}
