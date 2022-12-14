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
    private Customer[] _customers = new Customer[10];

    public Supermarket() 
    {
        for (int i = 0; i < _customers.Length; i++)
        {
            _customers[i] = new Customer();
        }
    }

    public void Start()
    {
        foreach(Customer customer in _customers)
        {
            _boxOffice.ServeNextCustomer(customer);
        }
    }
}

class BoxOffice
{
    private int _money = 0;
    private int _costOfProductsClient = 0;
    private int _customerCount = 0;

    public void ServeNextCustomer(Customer customer)
    {
        _customerCount++;

        Console.WriteLine($"Клиент №{_customerCount}");

        _costOfProductsClient = customer.GetCostOfProductsInBasket();
        _money += customer.PayProducts(_costOfProductsClient);

        Console.WriteLine($"Покупка на {_costOfProductsClient} оплачена.");
    }
}

class Customer
{
    private Random _random;
    private Basket _basket = new Basket();

    private int _money;
    private bool _areProductPaidFor;
    public Customer()
    {
        int minimumMoney = 500;
        int maximumMoney = 2001;

        _random = new Random();
        _money = _random.Next(minimumMoney, maximumMoney);
        _areProductPaidFor = false;
    }

    public int PayProducts(int price)
    {
        while (_areProductPaidFor == false)
        {
            if (_money >= price)
            {
                _money -= price;
                _areProductPaidFor = true;
            }
            else
            {
                Console.WriteLine("Нехватает денег");

                EjectRandomProduct();
            }
        }

        return price;
    }

    public int GetCostOfProductsInBasket()
    {
        int costOfProduct = _basket.GetCostOfProducts();

        return costOfProduct;
    }

    private void EjectRandomProduct()
    {
        _basket.EjectOneProduct();
    }
}

class Basket
{
    private List<Product> _products = new List<Product>();
    private Random _random;

    public Basket()
    {
        _random = new Random();

        FillWithProduct();
    }

    public void EjectOneProduct()
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

    private void FillWithProduct()
    {
        int minimumNumberProduct = 3;
        int maximumNumberProduct = 7;

        int randomProductCount = _random.Next(minimumNumberProduct, maximumNumberProduct);
        int randomProductNumber;

        for (int i = 0; i < randomProductCount; i++)
        {
            int minimumNumber = 0;
            int maximumNumber = 10;
            randomProductNumber = _random.Next(minimumNumber, maximumNumber);

            switch (randomProductNumber)
            {
                case 0:
                    _products.Add(new Bread());
                    break;
                case 1:
                    _products.Add(new Butter());
                    break;
                case 2:
                    _products.Add(new lemon());
                    break;
                case 3:
                    _products.Add(new Burger());
                    break;
                case 4:
                    _products.Add(new Beer());
                    break;
                case 5:
                    _products.Add(new Salad());
                    break;
                case 6:
                    _products.Add(new Vodka());
                    break;
                case 7:
                    _products.Add(new Beef());
                    break;
                case 8:
                    _products.Add(new Sausage());
                    break;
                case 9:
                    _products.Add(new ToiletPaper());
                    break;
            }
        }
    }
}

abstract class Product
{
    public string Name { get; protected set; }
    public int Price { get; protected set; }
}

class Bread : Product
{
    public Bread()
    {
        Name = "Хлеб";
        Price = 30;
    }
}

class Butter : Product
{
    public Butter()
    {
        Name = "Сливочное масло";
        Price = 90;
    }
}

class lemon : Product
{
    public lemon()
    {
        Name = "Лимон";
        Price = 15;
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

class ToiletPaper : Product
{
    public ToiletPaper()
    {
        Name = "Туалетная бумага";
        Price = 95;
    }
}