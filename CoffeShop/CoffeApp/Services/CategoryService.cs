using CoffeApp.Interfaces;
using CoffeApp.Models;
using CoffeApp.Repositories;

namespace CoffeApp.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> FindAll() {
           return _categoryRepository.FindAll();
        }

    
    }
}
