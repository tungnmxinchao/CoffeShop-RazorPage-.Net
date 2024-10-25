using System.ComponentModel.DataAnnotations;

namespace CoffeApp.DTOs
{
	public class AddProductRequest
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Product name is required")]
		public string Name { get; set; } = null!;

		[Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Description name is required")]
		public string? Description { get; set; }

		[Required(ErrorMessage = "Image is required")]
		public IFormFile? Image { get; set; }

		[Required(ErrorMessage = "Category name is required")]
		public int? CategoryId { get; set; }

		public string? UrlImage {  get; set; }
	}
}
