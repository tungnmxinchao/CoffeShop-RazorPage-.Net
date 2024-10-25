using CoffeApp.Models;

namespace CoffeApp.Interfaces
{
    public interface IOrderRepository
    {
        public List<Order> FindAll();

        public Order FindById(int id);

        public Order? FindOrderLastesByTableNumber(int phoneNumber);

        public List<Order> FindOrderByPhone(string phoneNumber);

        public bool InsertOrder(Order order);

        public bool UpdateOrder(Order order);
    }
}
