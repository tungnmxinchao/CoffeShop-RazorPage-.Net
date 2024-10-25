
using CoffeApp.Models;
using CoffeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoffeApp.Pages.Guest
{
    public class TrackingOrderStatusModel : PageModel
    {
        private readonly OrderService orderService;

        private readonly OrderDetailsService orderDetailsService;

		public TrackingOrderStatusModel(OrderService orderService, OrderDetailsService orderDetailsService)
		{
			this.orderService = orderService;
			this.orderDetailsService = orderDetailsService;
		}

		[BindProperty]
		public string PhoneNumber { get; set; }
        [BindProperty]
        public int OrderId { get; set; }

		public List<Order> ListOrder { get; set; }

		public List<OrderDetail> OrderDetails { get; set; }

		public void OnGet()
        {
			
        }

        public IActionResult OnPostSearchOrders() {

			ListOrder = orderService.FindOrdersByPhone(PhoneNumber);

			return Page();
		}

		public IActionResult OnPostOrder() {

            OrderDetails = orderDetailsService.FindOrderDetailsByOrderId(OrderId);
			ListOrder = orderService.FindOrdersByPhone(PhoneNumber);
			return Page();
		}
    }
}
