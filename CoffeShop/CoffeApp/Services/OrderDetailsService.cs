using CoffeApp.Interfaces;
using CoffeApp.Models;

namespace CoffeApp.Services
{
	public class OrderDetailsService
	{
		private readonly IOrderDetailsRepository _orderDetailsRepository;

		public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository)
		{
			_orderDetailsRepository = orderDetailsRepository;
		}

		public List<OrderDetail> FindOrderDetailsByOrderId(int orderId)
		{
			return _orderDetailsRepository.FindByOrderId(orderId);
		}

	}
}
