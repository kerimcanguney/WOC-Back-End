using API.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category AddCategory(Category category);
        Category? GetCategory(string id);
        Category updateCategory(string id, Category category);
        void deleteCategory(string id);
    }
}
