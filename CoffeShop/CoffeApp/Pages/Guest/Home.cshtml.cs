using CoffeApp.Models;
using CoffeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CoffeApp.Pages.Guest
{
    public class HomeModel : PageModel
    {
		private readonly OrderDetailStorageService _orderDetailStorageService;

        private readonly ProductService _productService;

        private readonly CategoryService _categoryService;

        public List<Product> listProduct {  get; set; }
        public List<Category> listCategory { get; set; }

		[BindProperty(SupportsGet = true)]
		public string SelectedCategory { get; set; }

		[BindProperty(SupportsGet = true)]
		public string SearchTerm { get; set; }

		public HomeModel(OrderDetailStorageService orderDetailStorageService, ProductService productService, CategoryService categoryService)
		{
			_orderDetailStorageService = orderDetailStorageService;
            _productService = productService;
			_categoryService = categoryService;

        }

		public void OnGet()
        {
            SetupSessionOrderDetails();

            listCategory = _categoryService.FindAll();

            listProduct = GetProducts();

		}

        private void SetupSessionOrderDetails()
        {
            var sessionOrderDetails = HttpContext.Session.GetString("OrderDetails");
            if (string.IsNullOrEmpty(sessionOrderDetails))
            {
                var orderDetailsJson = JsonConvert.SerializeObject(_orderDetailStorageService.MapOrderDetails);
                HttpContext.Session.SetString("OrderDetails", orderDetailsJson);
            }
        }

        private List<Product> GetProducts()
        {
            if (string.IsNullOrEmpty(SearchTerm))
            {
                return _productService.FindAll();
            }

            if (string.IsNullOrEmpty(SelectedCategory) || SelectedCategory.Equals("All"))
            {
                return _productService.FindProductByName(SearchTerm);
            }

            int categoryId = int.TryParse(SelectedCategory, out var parsedId) ? parsedId : 0;
            return _productService.SearchByNameAndCategory(SearchTerm, categoryId);
        }
    }
}
