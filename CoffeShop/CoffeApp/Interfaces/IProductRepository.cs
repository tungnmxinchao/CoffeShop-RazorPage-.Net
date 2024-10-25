using CoffeApp.Models;

namespace CoffeApp.Interfaces
{
    public interface IProductRepository
    {
        public List<Product> FindAll();

        public Product FindById(int id);

        public List<Product> FindByName(string name);

        public bool AddProduct(Product product);

        public bool UpdateProduct(Product product);

        public bool DeleteProduct(int id);

        public List<Product> SearchByNameAndCategory(string categoryName, int categoryId);
    }
}
