using CoffeApp.Interfaces;
using CoffeApp.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;


namespace CoffeApp.Services
{
    public class CartService
    {
        private readonly IProductRepository _productRepository;


        public CartService(IProductRepository productRepository)
        {

            _productRepository = productRepository;
        }

        public bool AddToCart(Dictionary<int, OrderDetail> cartItems, int productId)
        {
            if (cartItems == null) throw new ArgumentNullException(nameof(cartItems));

            if (cartItems.TryGetValue(productId, out var orderDetail))
            {
                orderDetail.Quantity += 1;
            }
            else
            {
                var product = _productRepository.FindById(productId);
                if (product == null)
                {
                    return false; 
                }

                var newOrderDetail = new OrderDetail
                {
                    ProductId = productId,
                    Quantity = 1,
                    Product = product
                };

                cartItems.Add(productId, newOrderDetail);
            }

            return true; 
        }

        public bool RemoveFromCart(Dictionary<int, OrderDetail> cartItems, int productId)
        {
            if (cartItems == null) throw new ArgumentNullException(nameof(cartItems));

            return cartItems.Remove(productId);
        }

        public void UpdateQuantities(Dictionary<int, OrderDetail> cartItems, string quantities)
        {
            if (cartItems == null) throw new ArgumentNullException(nameof(cartItems));

            var quantityArray = quantities.Split(',');
            int index = 0;

            foreach (var orderDetailsEntry in cartItems)
            {
                var orderDetail = orderDetailsEntry.Value;

                if (index < quantityArray.Length && int.TryParse(quantityArray[index], out int newQuantity))
                {
                    orderDetail.Quantity = newQuantity;
                }

                index++;
            }
        }
    }
}
