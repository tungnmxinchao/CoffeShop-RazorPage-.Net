using CoffeApp.Interfaces;
using CoffeApp.Models;

namespace CoffeApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public List<Order> FindAll()
        {
            return PRN221_CoffeShopContext.Ins.Orders.ToList();
        }

        public Order FindById(int id)
        {
            var order = PRN221_CoffeShopContext.Ins.Orders.Find(id);

            if (order != null)
            {
                return order;
            }
            return null;
        }

        public List<Order> FindOrderByPhone(string phoneNumber)
        {
            var orders = PRN221_CoffeShopContext.Ins.Orders
                .Where(o => o.Phone.Equals(phoneNumber)).OrderByDescending( o => o.OrderDate).ToList();

            return orders;
        }

        public Order? FindOrderLastesByTableNumber(int tableNumber)
        {
            var order = PRN221_CoffeShopContext.Ins.Orders
                 .Where(o => o.TableNumber == tableNumber)
                 .OrderByDescending(o => o.OrderDate).FirstOrDefault();

            if(order != null)
            {
				return order;
			}
            return null;
           
        }

        public bool InsertOrder(Order order)
        {
            try
            {
                PRN221_CoffeShopContext.Ins.Orders.Add(order);
                PRN221_CoffeShopContext.Ins.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error inserting order: {ex.Message}");
                return false;
            }
        }

        public bool UpdateOrder(Order order)
        {
            try
            {
                var existingOrder = PRN221_CoffeShopContext.Ins.Orders.Find(order.OrderId);
                if (existingOrder != null)
                {

                    existingOrder.TableNumber = order.TableNumber;
                    existingOrder.Phone = order.Phone;
                    existingOrder.OrderDate = order.OrderDate;
                    existingOrder.TotalAmount = order.TotalAmount;
                    existingOrder.Status = order.Status;

                    PRN221_CoffeShopContext.Ins.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error updating order: {ex.Message}");
                return false;
            }
        }
    }
}
