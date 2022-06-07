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
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }
        [HttpPost, Route("/post")]
        public IActionResult add(string name, string category, string type)
        {
            //if (Enum.IsDefined(typeof(ProductEnums.ProductTypeEnum), type) == false)  return null; // type bestaat niet
            //if (Enum.IsDefined(typeof(ProductEnums.ProductCategoryEnum), category) == false)  return null; // category bestaat niet
            //Switch naar check if category / type exists in db

            //ProductEnums.ProductTypeEnum ProductType = (ProductEnums.ProductTypeEnum)Enum.Parse(typeof(ProductEnums.ProductTypeEnum), type);
            //ProductEnums.ProductCategoryEnum ProductCategory = (ProductEnums.ProductCategoryEnum)Enum.Parse(typeof(ProductEnums.ProductCategoryEnum), category);
            if (type == null || type == "" || category == null || category == "")
            {
                throw new InvalidOperationException("Invalid info");
            }

            List<Category> categories = _categoryService.GetCategories();
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Name == category) //Check if category is valid category
                {
                    for (int z = 0; z < categories[i].Types.Count; z++)
                    {
                        if (categories[i].Types[z].Name == type) //Check if type name exists in category
                        {
                            Product product = new() { Name = name, Category = category, Type = type, Info = categories[i].Types[z].Info };
                            return Ok(_productService.AddProduct(product));
                        }
                    }
                }
            }
            throw new InvalidOperationException("Invalid info");
        }

        [HttpGet, Route("/getbyid")]
        public IActionResult getById(string id)
        {
            return Ok(_productService.GetProduct(id));
        }
        [HttpPut, Route("/updateCategoryOfProduct")]
        public IActionResult updateCategoryOfProduct(string productid, string categoryid)
        {
            if (categoryid == null || categoryid == "")
            {
                throw new InvalidOperationException("Invalid info");
            }
            Product product = _productService.GetProduct(productid);
            Category category = _categoryService.GetCategory(categoryid);
            if (category.Types == null)
            {
                throw new InvalidOperationException("Category does not contain type");
            }
            else
            {
                product.Category = category.Name;
                product.Type = category.Types[0].Name; //Set first type of that category
                product.Info = category.Types[0].Info; //Take info of that type as info attributes
                _productService.updateProduct(productid, product);
            }
            return Ok("Done");
        }
        [HttpPut, Route("/updateTypeOfProduct")]
        public IActionResult updateTypeOfProduct(string productid, string categoryid, string type)
        {
            if (type == null || type == "" || categoryid == null || categoryid == "")
            {
                throw new InvalidOperationException("Invalid info");
            }
            Product product = _productService.GetProduct(productid);
            Category category = _categoryService.GetCategory(categoryid);
            if (category.Types == null)
            {
                throw new InvalidOperationException("Invalid action");
            }
            else
            {
                for (int i = 0; i < category.Types.Count; i++)
                {
                    if (category.Types[i].Name == type) // Check if type exists in categories
                    {
                        product.Type = type; 
                        product.Info = category.Types[i].Info; //Take info of that type as info attributes
                        _productService.updateProduct(productid, product);
                    }
                }
            }
            return Ok("Done");
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

        [HttpGet, Route("/categories")]
        public IActionResult getCategories()
        {
            return Ok(_categoryService.GetCategories());
        }
        [HttpGet, Route("/getcategorybyid")]
        public IActionResult getCategoryById(string id)
        {
            return Ok(_categoryService.GetCategory(id));
        }
        [HttpPost, Route("/addCategory")]
        public IActionResult addCategory(string name)
        {
            if (name == null || name == "")
            {
                throw new InvalidOperationException("Invalid info");
            }
            List<Category> categories = _categoryService.GetCategories();
            for (int i = 0; i < categories.Count; i++)
            {
                if (categories[i].Name == name)
                {
                    throw new InvalidOperationException("Name already exists");
                }
            }
            Category category = new() { Name = name};
            return Ok(_categoryService.AddCategory(category));
        }
        [HttpPost, Route("/addTypeToCategory")]
        public IActionResult addTypeToCategory(string categoryid, string type)
        {
            if (type == null || type == "")
            {
                throw new InvalidOperationException("Invalid info");
            }
            Category category = _categoryService.GetCategory(categoryid);
            for (int i = 0; i < category.Types.Count; i++)
            {
                if (category.Types[i].Name == type)
                {
                    throw new InvalidOperationException("Name already exists");
                }
            }
            if (category.Types == null) //If types is empty
            {
                List<ProductModels.Type> types = new();
                types.Add(new ProductModels.Type() { Name = type });
                category.Types = types;
            }
            else
            {
                category.Types.Add(new ProductModels.Type() { Name = type});
            }
            return Ok(_categoryService.updateCategory(categoryid, category));
        }
        [HttpPost, Route("/addInfoToTypeInCategory")]
        public IActionResult addInfoToTypeInCategory(string categoryid, string type, string name)
        {
            if (type == null || type == "" || name == null || name == "")
            {
                throw new InvalidOperationException("Invalid info");
            }
            Category category = _categoryService.GetCategory(categoryid);
            if (category.Types == null) //If types is empty
            {
                //Error
            }
            else
            {
                for (int i = 0; i < category.Types.Count; i++)
                {
                    if (category.Types[i].Name == type)
                    {
                        if (category.Types[i].Info != null)
                        {
                            for (int z = 0; z < category.Types[i].Info.Count; z++)
                            {
                                if (category.Types[i].Info[z].Name == name)
                                {
                                    throw new InvalidOperationException("Name already exists");
                                }
                            }
                            category.Types[i].Info.Add(new Info() { Name = name, Value = "" });
                        }
                        if (category.Types[i].Info == null)
                        {
                            List<Info> info = new();
                            info.Add(new Info() { Name = name, Value = "" });
                            category.Types[i].Info = info;
                        }
                    }
                }
            }
            return Ok(_categoryService.updateCategory(categoryid, category));
        }
        [HttpPost, Route("/removeTypeFromCategory")]
        public IActionResult removeTypeFromCategory(string categoryid, string type)
        {
            Category category = _categoryService.GetCategory(categoryid);
            if (category.Types == null) //If types is empty
            {
                //Error
            }
            else
            {
                for (int i = 0; i < category.Types.Count; i++)
                {
                    if (category.Types[i].Name == type)
                    {
                        category.Types.RemoveAt(i);
                    }
                }
            }
            return Ok(_categoryService.updateCategory(categoryid, category));
        }
        [HttpPost, Route("/removeInfoFromTypeInCategory")]
        public IActionResult removeInfoFromTypeInCategory(string categoryid, string type, string name)
        {
            Category category = _categoryService.GetCategory(categoryid);
            if (category.Types == null) //If types is empty
            {
                //Error
            }
            else
            {
                for (int i = 0; i < category.Types.Count; i++)
                {
                    if (category.Types[i].Name == type)
                    {
                        for (int z = 0; z < category.Types[i].Info.Count; z++)
                        {
                            if (category.Types[i].Info[z].Name == name)
                            {
                                category.Types[i].Info.RemoveAt(z);
                            }
                        }
                    }
                }
            }
            return Ok(_categoryService.updateCategory(categoryid, category));
        }
        [HttpPut, Route("/updateCategory")]
        public IActionResult updateCategory(string id, Category category)
        {
            category.Id = id;
            _categoryService.updateCategory(id, category);
            return Ok("Done");
        }
        [HttpDelete, Route("/deleteCategory")]
        public IActionResult deleteCategory(string id)
        {
            _categoryService.deleteCategory(id);
            return Ok("Done");
        }
    }
}
