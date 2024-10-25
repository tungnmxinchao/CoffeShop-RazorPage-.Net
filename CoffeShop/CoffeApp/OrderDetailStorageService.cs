//using DataAccess.Models;

using CoffeApp.Models;

namespace CoffeApp
{
	public class OrderDetailStorageService
	{
		public Dictionary<int, OrderDetail> MapOrderDetails {  get; set; }

		public OrderDetailStorageService()
		{
			MapOrderDetails = new Dictionary<int, OrderDetail>();	
		}
	}
}
