using CoffeApp.Interfaces;
using CoffeApp.Models;
using CoffeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CoffeApp.Pages.Guest
{
    public class DeleteCartModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int productId { get; set; }

		private readonly OrderDetailStorageService _orderDetailsStorage;


        private readonly CartService _cartService;

        public DeleteCartModel(OrderDetailStorageService orderDetailsStorage,CartService cartService)
		{
			_orderDetailsStorage = orderDetailsStorage;
            _cartService = cartService; 
        }

		public IActionResult OnGet()
        {
            var sessionOrderDetails = HttpContext.Session.GetString("OrderDetails");

			if (!string.IsNullOrEmpty(sessionOrderDetails))
			{
				_orderDetailsStorage.MapOrderDetails = JsonConvert.DeserializeObject<Dictionary<int, Models.OrderDetail>>(sessionOrderDetails);
            }

            if (_cartService.RemoveFromCart(_orderDetailsStorage.MapOrderDetails, productId))
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var orderDetailsJson = JsonConvert.SerializeObject(_orderDetailsStorage.MapOrderDetails, settings);
                HttpContext.Session.SetString("OrderDetails", orderDetailsJson);
            }

            return RedirectToPage("/Guest/ViewCart");


        }
    }
}
