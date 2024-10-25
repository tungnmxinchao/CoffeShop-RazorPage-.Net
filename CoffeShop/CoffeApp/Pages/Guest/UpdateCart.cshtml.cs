
using CoffeApp.Models;
using CoffeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CoffeApp.Pages.Guest
{
    public class UpdateCartModel : PageModel
    {
		private readonly OrderDetailStorageService _orderDetailStorage;

        private readonly CartService _cartService;

        public UpdateCartModel(OrderDetailStorageService orderDetailStorageService, CartService cartService)
		{
			_orderDetailStorage = orderDetailStorageService;
            _cartService = cartService;
        }

		[BindProperty(SupportsGet =true)]
		public string Quantities { get; set; }

		public IActionResult OnGet()
        {

			var sessionOrderDetails = HttpContext.Session.GetString("OrderDetails");

			if (!string.IsNullOrEmpty(sessionOrderDetails))
			{
				_orderDetailStorage.MapOrderDetails = JsonConvert.DeserializeObject<Dictionary<int, OrderDetail>>(sessionOrderDetails);
			}

            _cartService.UpdateQuantities(_orderDetailStorage.MapOrderDetails, Quantities);

            var updatedOrderDetailsJson = JsonConvert.SerializeObject(_orderDetailStorage.MapOrderDetails);
			HttpContext.Session.SetString("OrderDetails", updatedOrderDetailsJson);

			return RedirectToPage("/Guest/ViewCart");
		}
    }
}
