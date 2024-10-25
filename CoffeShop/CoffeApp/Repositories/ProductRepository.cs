using CoffeApp.Interfaces;
using CoffeApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public bool AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> FindAll()
        {
            return PRN221_CoffeShopContext.Ins.Products.Include(x => x.Category).ToList();
        }

        public Product FindById(int id)
        {
            var product = PRN221_CoffeShopContext.Ins.Products.Find(id);

            if (product != null)
            {
                return product;
            }

            return null;

        }

        public List<Product> FindByName(string name)
        {
            return PRN221_CoffeShopContext.Ins.Products.Include(x => x.Category)
                .Where(x => x.Name.Contains(name)).ToList();
        }

        public List<Product> SearchByNameAndCategory(string productName, int categoryId)
        {
            return PRN221_CoffeShopContext.Ins.Products
                .Include(p => p.Category)
                .Where(p => (string.IsNullOrEmpty(productName) || p.Name.Contains(productName)) &&
                            (categoryId == 0 || p.CategoryId == categoryId))
                .ToList();
        }


        public bool UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
