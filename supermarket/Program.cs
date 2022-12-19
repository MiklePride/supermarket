﻿using System;
using System.Collections.Generic;

namespace supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();

            supermarket.Work();

            Console.ReadLine();
        }
    }
}

class Supermarket
{
    private BoxOffice _boxOffice = new BoxOffice();
    private Queue<Customer> _customers = new Queue<Customer>();
    private List<Product> _products = new List<Product>();

    public Supermarket()
    {
        int numberOfProductsSameType = 10;

        for (int i = 0; i < numberOfProductsSameType; i++)
        {
            _products.Add(new Product("Масло", 92));
            _products.Add(new Product("Пиво", 80));
            _products.Add(new Product("Мясо", 200));
            _products.Add(new Product("Молоко", 153));
            _products.Add(new Product("Приправа", 48));
            _products.Add(new Product("Яйца", 100));
            _products.Add(new Product("Квас", 85));
        }

        int numberCustomer = 10;

        for (int i = 0; i < numberCustomer; i++)
        {
            Customer customer = new Customer();
            customer.CollectProducts(_products);

            _customers.Enqueue(customer);
        }
    }

    public void Work()
    {
        while (_customers.Count > 0)
        {
            _boxOffice.ServeNextCustomer(_customers.Dequeue());
        }
    }
}

class BoxOffice
{
    private int _money = 0;
    private int _customerCount = 0;

    public void ServeNextCustomer(Customer customer)
    {
        bool isCustomerServed = false;
        _customerCount++;

        Console.WriteLine($"Клиент №{_customerCount}");

        while (isCustomerServed == false)
        {
            int costOfProductsClient = customer.GetCostOfProductsInBasket();

            if (customer.IsEnoughMoney(costOfProductsClient))
            {
                _money += customer.PayProducts(costOfProductsClient);

                Console.WriteLine($"Покупка на {costOfProductsClient} оплачена. В кассе {_money}руб.");

                isCustomerServed = true;
            }
            else
            {
                Console.WriteLine("Нехватает денег!");
                customer.EjectRandomProduct();
            }
        }
    }
}

class Customer
{
    private static Random _random = new Random();
    private Basket _basket = new Basket();

    private int _money;

    public Customer()
    {
        int minimumMoney = 300;
        int maximumMoney = 1000;

        _money = _random.Next(minimumMoney, maximumMoney);
    }

    public void CollectProducts(List<Product> products)
    {
        int minimumIndexProduct = 0;
        int numberPlaceInBasket = 5;

        for (int i = 0; i < numberPlaceInBasket; i++)
        {
            int maximumIndexProduct = products.Count;
            int indexProduct = _random.Next(minimumIndexProduct, maximumIndexProduct);

            _basket.AddProduct(products[i].GetClone());
        }
    }

    public bool IsEnoughMoney(int price)
    {
        return _money >= price;
    }

    public int PayProducts(int price)
    {
        int moneyOfProducts = price;

        _money -= moneyOfProducts;

        return moneyOfProducts;
    }

    public int GetCostOfProductsInBasket()
    {
        return _basket.GetCostOfProducts();
    }

    public void EjectRandomProduct()
    {
        _basket.EjectProduct();
    }
}

class Basket
{
    private List<Product> _products = new List<Product>();
    private static Random _random = new Random();

    public void EjectProduct()
    {
        int minimumIndexProduct = 0;
        int indexProductToDelete = _random.Next(minimumIndexProduct, _products.Count);

        Console.WriteLine($"Выброшу {_products[indexProductToDelete].Name}.");

        _products.RemoveAt(indexProductToDelete);
    }

    public int GetCostOfProducts()
    {
        int costOfProducts = 0;

        foreach (Product product in _products)
        {
            costOfProducts += product.Price;
        }

        return costOfProducts;
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }
}

class Product
{
    public string Name { get; private set; }
    public int Price { get; private set; }

    public Product(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public Product GetClone()
    {
        return new Product(Name, Price);
    }
}
