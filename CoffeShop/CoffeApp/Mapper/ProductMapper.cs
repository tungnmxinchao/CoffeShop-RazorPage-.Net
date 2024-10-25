using CoffeApp.DTOs;
using CoffeApp.Models;

namespace CoffeApp.Mapper
{
	public class ProductMapper
	{
		public void MapToProduct(Product product, AddProductRequest addProductRequest)
		{
			product.Name = addProductRequest.Name;
			product.Price = addProductRequest.Price;
			product.Description = addProductRequest.Description;
			product.CategoryId = addProductRequest.CategoryId;
			product.Image = addProductRequest.UrlImage;

		}
	}
}
