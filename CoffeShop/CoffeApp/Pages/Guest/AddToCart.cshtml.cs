using CoffeApp.Models;
using CoffeApp.Services;
//using DataAccess;
//using DataAccess.Models;
//using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CoffeApp.Pages.Guest
{
    public class AddToCartModel : PageModel
    {

		private readonly OrderDetailStorageService _orderDetailStorageService;
        private readonly CartService _cartService;

		[BindProperty(SupportsGet = true)]
		public int ProductId { get; set; }

		public AddToCartModel(OrderDetailStorageService orderDetailStorageService, CartService cartService)
		{
			_orderDetailStorageService = orderDetailStorageService;
            _cartService = cartService;

        }

        public IActionResult OnGet()
        {
            var sessionOrderDetails = HttpContext.Session.GetString("OrderDetails");
            if (!string.IsNullOrEmpty(sessionOrderDetails))
            {
                _orderDetailStorageService.MapOrderDetails = JsonConvert.DeserializeObject<Dictionary<int, OrderDetail>>(sessionOrderDetails);
            }

            if (_cartService.AddToCart(_orderDetailStorageService.MapOrderDetails, ProductId))
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                var orderDetailsJson = JsonConvert.SerializeObject(_orderDetailStorageService.MapOrderDetails, settings);
                HttpContext.Session.SetString("OrderDetails", orderDetailsJson);
               
            }
            return RedirectToPage("/Guest/ViewCart");

        }

    }
}
