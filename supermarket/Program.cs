using System;
using System.Collections.Generic;

namespace supermarket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();

            supermarket.Start();

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
            _products.Add(new Butter());
            _products.Add(new Burger());
            _products.Add(new Beer());
            _products.Add(new Beef());
            _products.Add(new Sausage());
            _products.Add(new Salad());
            _products.Add(new Vodka());
        }

        int numberCustomer = 10;

        for (int i = 0; i < numberCustomer; i++)
        {
            Customer customer = new Customer();
            customer.CollectProduct(_products);

            _customers.Enqueue(customer);
        }
    }

    public void Start()
    {
        while(_customers.Count > 0)
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

    public void CollectProduct(List<Product> products)
    {
        int minimumIndexProduct = 0;
        int numberPlaceInBasket = 5;

        for (int i = 0; i < numberPlaceInBasket; i++)
        {
            int maximumIndexProduct = products.Count;
            int indexProduct = _random.Next(minimumIndexProduct, maximumIndexProduct);

            Product product = products[indexProduct];

            _basket.AddProduct(product);

            products.RemoveAt(indexProduct);
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
        int costOfProduct = _basket.GetCostOfProducts();

        return costOfProduct;
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

abstract class Product
{
    public string Name { get; protected set; }
    public int Price { get; protected set; }
}

class Butter : Product
{
    public Butter()
    {
        Name = "Сливочное масло";
        Price = 90;
    }
}

class Burger : Product
{
    public Burger()
    {
        Name = "Бургер";
        Price = 100;
    }
}

class Beer : Product
{
    public Beer()
    {
        Name = "Пиво";
        Price = 120;
    }
}

class Salad : Product
{
    public Salad()
    {
        Name = "Салат";
        Price = 80;
    }
}

class Vodka : Product
{
    public Vodka()
    {
        Name = "Водка";
        Price = 200;
    }
}

class Beef : Product
{
    public Beef()
    {
        Name = "Говядина";
        Price = 250;
    }
}

class Sausage : Product
{
    public Sausage()
    {
        Name = "Сосиски";
        Price = 110;
    }
}