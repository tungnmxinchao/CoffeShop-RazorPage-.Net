﻿using CoffeApp.Interfaces;
using CoffeApp.Models;
using CoffeApp.Repositories;

namespace CoffeApp.Services
{

    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> FindAll()
        {
            return _productRepository.FindAll();
        }

        public List<Product> FindProductByName(string name)
        {
            return _productRepository.FindByName(name);
        }

        public List<Product> SearchByNameAndCategory(string name, int categoryId)
        {
            return _productRepository.SearchByNameAndCategory(name, categoryId);
        }
    }
}