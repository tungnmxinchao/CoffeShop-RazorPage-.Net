using CoffeApp.Models;

namespace CoffeApp.Interfaces
{
    public interface ICategoryRepository
    {
        public List<Category> FindAll();
    }
}
