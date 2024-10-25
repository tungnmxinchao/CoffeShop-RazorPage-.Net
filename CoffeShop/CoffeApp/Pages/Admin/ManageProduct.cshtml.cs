using CoffeApp.DTOs;
using CoffeApp.Mapper;
using CoffeApp.Models;
using CoffeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeApp.Pages.Admin
{
    public class ManageProductModel : PageModel
    {
        private readonly ProductService productService;

		private readonly IWebHostEnvironment _environment;

		private readonly ProductMapper productMapper;

		public ManageProductModel(ProductService productService, IWebHostEnvironment environment, ProductMapper productMapper)
		{
			this.productService = productService;
			_environment = environment;
			this.productMapper = productMapper;
		}


		public List<Product> Products { get; set; }

		[BindProperty]
		public AddProductRequest AddProduct { get; set; }

		public string ErrorMsg { get; set; }

		public string SuccessMsg { get; set; }

		

		public IActionResult OnGet()
        {
			Products = productService.FindAll();

			return Page();

		}

		public async Task<IActionResult> OnPostAddProduct()
		{
			if (!ModelState.IsValid)
			{
				Products = productService.FindAll();
				return Page();
			}

			string? imagePath = await SaveImageAsync(AddProduct.Image);
			if (imagePath == null)
			{
				ErrorMsg = "Image upload failed";
				return Page();
			}

			AddProduct.UrlImage = imagePath;
			var product = productService.FindProductById(AddProduct.Id);

			if (product == null)
			{
				ErrorMsg = "Product not found!";
				return Page();
			}

			productMapper.MapToProduct(product, AddProduct);

			if (productService.AddProduct(product))
			{
				SuccessMsg = "Product added successfully";
			}
			else
			{
				ErrorMsg = "Failed to add product";
			}

			Products = productService.FindAll();  
			return Page();
		}

		private async Task<string?> SaveImageAsync(IFormFile imageFile)
		{
			if (imageFile == null) return null;

			try
			{
				var uniqueFileName = $"{Path.GetFileNameWithoutExtension(imageFile.FileName)}_{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
				var filePath = Path.Combine(_environment.WebRootPath, "uploads", uniqueFileName);

				Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
				using var fileStream = new FileStream(filePath, FileMode.Create);
				await imageFile.CopyToAsync(fileStream);

				return $"/uploads/{uniqueFileName}";
			}
			catch
			{
				return null; 
			}
		}
	}
}
