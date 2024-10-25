using CoffeApp.Models;

namespace CoffeApp.Interfaces
{
    public interface IOrderDetailsRepository
    {
        public List<OrderDetail> FindByOrderId(int orderId);
    }
}
