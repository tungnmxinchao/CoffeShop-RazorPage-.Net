
using CoffeApp.Models;
using CoffeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CoffeApp.Pages.Guest
{
	public class ViewCartModel : PageModel
	{
		private readonly OrderDetailStorageService _orderDetailsStorage;

		private readonly OrderService _orderService;

		public Dictionary<int, OrderDetail> OrderDetailsMap { get; private set; }

		[BindProperty]
		public decimal TotalAmount { get; set; }

		[BindProperty]
		public int TableNumber { get; set; }

		[BindProperty]
		public string PhoneNumber { get; set; }

		public string ErrorMessage { get; set; }

		public string SuccessMessage {  get; set; }


		public ViewCartModel(OrderDetailStorageService orderDetailStorageService, OrderService orderService)
		{
			_orderDetailsStorage = orderDetailStorageService;
            _orderService = orderService;
		}

		public void OnGet()
		{
			LoadCartFromSession();
		}

		public IActionResult OnPostCheckout()
		{

			var sessionOrderDetails = HttpContext.Session.GetString("OrderDetails");

			var orderDetailsMap = JsonConvert.DeserializeObject<Dictionary<int, OrderDetail>>(sessionOrderDetails);

			if (orderDetailsMap == null || orderDetailsMap.Count <= 0)
			{
				ErrorMessage = "Your cart is empty.";
				return Page();
			}

			OrderDetailsMap = orderDetailsMap;

			if (!ValidateCheckout())
			{
				return Page();
			}

				

			var newOrder = new Order
			{
				TableNumber = TableNumber,
				Phone = PhoneNumber,
				OrderDate = DateTime.Now,
				TotalAmount = TotalAmount,
				Status = "Pending",
				OrderDetails = orderDetailsMap.Select(od => new OrderDetail
				{
					ProductId = od.Value.ProductId,
					Quantity = od.Value.Quantity,
					Price = od.Value.Price
				}).ToList()
			};

			if (_orderService.InsertOrder(newOrder))
			{
				SuccessMessage = "Checkout Successfully!";

				HttpContext.Session.Remove("OrderDetails");
				OrderDetailsMap.Clear();
				_orderDetailsStorage.MapOrderDetails.Clear();

			}
			else
			{
				ErrorMessage = "Checkout Failed!";
            }


			return Page();
		}

		private void LoadCartFromSession()
		{
			var sessionOrderDetails = HttpContext.Session.GetString("OrderDetails");

			if (!string.IsNullOrEmpty(sessionOrderDetails))
			{
				_orderDetailsStorage.MapOrderDetails = JsonConvert.DeserializeObject<Dictionary<int, OrderDetail>>(sessionOrderDetails);
			}

			OrderDetailsMap = _orderDetailsStorage.MapOrderDetails;

			foreach (var orderDetailsEntry in OrderDetailsMap)
			{
				var orderDetails = orderDetailsEntry.Value;
				if (orderDetails != null)
				{
					orderDetails.Price = orderDetails.Product.Price * orderDetails.Quantity;
					TotalAmount += orderDetails.Price;
				}
			}

			var updateOrderDetailsJson = JsonConvert.SerializeObject(OrderDetailsMap);
			HttpContext.Session.SetString("OrderDetails", updateOrderDetailsJson);
		}

		private bool ValidateCheckout()
		{
			if (TableNumber < 1 || TableNumber > 10)
			{
				ErrorMessage = "Table number must be between 1 and 10.";
				return false;
			}

			if (!IsValidPhoneNumber(PhoneNumber))
			{
				ErrorMessage = "Phone number is invalid. It should contain only digits and be between 9-11 characters.";
				return false;
			}

			if (DateTime.Now.Hour < 6 || DateTime.Now.Hour > 22)
			{
				ErrorMessage = "Orders are only allowed between 6 AM and 10 PM.";
				return false;
			}

			var order = _orderService.FindLastesOrderByTableNumber(TableNumber);
			if (order != null && order.Status == "Pending")
			{
				ErrorMessage = "Table have been orderd by someone.";
				return false;
			}




			return true;
		}

		private bool IsValidPhoneNumber(string phoneNumber)
		{
			return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 9 && phoneNumber.Length <= 11;
		}
	}
}
