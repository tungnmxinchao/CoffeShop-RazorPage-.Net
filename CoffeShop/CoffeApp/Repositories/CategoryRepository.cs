using CoffeApp.Interfaces;
using CoffeApp.Models;

namespace CoffeApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> FindAll()
        {
            return PRN221_CoffeShopContext.Ins.Categories.ToList();
        }
    }
}
