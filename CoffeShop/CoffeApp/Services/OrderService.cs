using CoffeApp.Interfaces;
using CoffeApp.Models;
using CoffeApp.Repositories;

namespace CoffeApp.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public bool InsertOrder(Order order)
        {
            if(order != null)
            {
                return _orderRepository.InsertOrder(order);
            }
            throw new Exception("Order not found!");
        }

        public bool UpdateOrder(Order order)
        {
            if(order != null)
            {
                return _orderRepository.UpdateOrder(order);
            }
            throw new Exception("Order not found!");
        }

        public Order? FindLastesOrderByTableNumber(int tableNumber)
        {
            var order =  _orderRepository.FindOrderLastesByTableNumber(tableNumber);

            if(order != null)
            {
                return order;
            }

            return null;
		}

        public List<Order> FindOrdersByPhone(string phone)
        {
            return _orderRepository.FindOrderByPhone(phone);
		}

        public Order? FindOrderById(int id)
        {
           var order = _orderRepository.FindById(id);

            if(order != null )
            {
                return order;
            }

            return null;

		}
    }
}
