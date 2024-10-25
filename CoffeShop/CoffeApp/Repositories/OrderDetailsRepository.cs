using CoffeApp.Interfaces;
using CoffeApp.Models;

namespace CoffeApp.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        public List<OrderDetail> FindByOrderId(int orderId)
        {
            var orderDetails = PRN221_CoffeShopContext.Ins.OrderDetails
                     .Where(od => od.OrderId == orderId).ToList();

            return orderDetails;
        }
    }
}
