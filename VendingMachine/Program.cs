using System;
using System.Collections.Generic;

class Program
{
    static List<Product> products = new List<Product>()
    {
        new Product("Добрая Кола", 9, 5),
        new Product("Чипсы Лэйс", 14, 10),
        new Product("Шоколадка", 6, 3),
        new Product("Жвачка Love Is", 1, 10)
    };

    static int userBalance = 0;

    static int totalRevenue = 0;

    static int[] coins = { 1, 2, 5, 10 };

    static void Main()
    {
        Console.WriteLine("---- ВЕНДИНГОВЫЙ АВТОМАТ ----");

        while (true)
        {
            Console.WriteLine("\n1 - Посмотреть товары");
            Console.WriteLine("2 - Вставить монету");
            Console.WriteLine("3 - Купить товар");
            Console.WriteLine("4 - Вернуть сдачу");
            Console.WriteLine("5 - Режим администратора");
            Console.WriteLine("0 - Выход");

            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ShowProducts(); break;
                case "2": InsertCoin(); break;
                case "3": BuyProduct(); break;
                case "4": ReturnChange(); break;
                case "5": AdminMode(); break;
                case "0": return;
                default: Console.WriteLine("Такой вариант выбора отсутствует."); break;
            }
        }
    }

    static void ShowProducts()
    {
        Console.WriteLine("\n---- ДОСТУПНЫЕ ТОВАРЫ ----");
        for (int i = 0; i < products.Count; i += 1)
        {
            Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price} руб. (осталось {products[i].Count} шт.)");
        }
        Console.WriteLine($"Ваш баланс: {userBalance} руб.");
    }

    static void InsertCoin()
    {
        Console.WriteLine("\nПринимаются монеты номиналом 1, 2, 5 и 10 руб. ");
        Console.Write("Введите номинал монеты: ");

        if (int.TryParse(Console.ReadLine(), out int coin))
        {
            bool validCoin = false;
            foreach (int c in coins)
            {
                if (c == coin)
                {
                    validCoin = true;
                    break;
                }
            }

            if (validCoin)
            {
                userBalance += coin;
                Console.WriteLine($"Монета принята! \nБаланс: {userBalance} руб.");
            }
            else
            {
                Console.WriteLine("Автомат не принимает монеты такого номинала.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный формат номинала монеты.");
        }
    }

    static void BuyProduct()
    {
        ShowProducts();
        Console.Write("Введите номер товара: ");

        string input = Console.ReadLine();
        int productNumber = 0;
        bool isNumber = true;

        foreach (char symbol in input)
        {
            if (!char.IsDigit(symbol))
            {
                isNumber = false; break;
            }
        }

        if (isNumber)
        {
            productNumber = int.Parse(input);
        }

        if (isNumber && productNumber > 0 && productNumber <= products.Count)
        {
            Product product = products[productNumber - 1];

            if (product.Count > 0)
            {
                if (userBalance >= product.Price)
                {
                    product.Count = product.Count - 1;
                    userBalance -= product.Price;
                    totalRevenue += product.Price;
                    Console.WriteLine($"Вы купили товар '{product.Name}'. \nВыдана сдача в размере {userBalance} руб.");
                    userBalance = 0;
                }
                else
                {
                    Console.WriteLine($"Недостаточно средств!\nВыберите другой товар или верните деньги (опция 4).");
                }
            }
            else
            {
                Console.WriteLine("Этот товар закончился.");
            }
        }
        else
        {
            Console.WriteLine("Товара с таким номером не существует.");
        }
    }

    static void ReturnChange()
    {
        if (userBalance > 0)
        {
            Console.WriteLine($"Выдана сдача: {userBalance} руб.");
            userBalance = 0;
        }
        else
        {
            Console.WriteLine("Сдачи нет");
        }
    }

    static void AdminMode()
    {
        Console.Write("Введите пароль администратора: ");
        string password = Console.ReadLine();

        if (password == "5555")
        {
            Console.WriteLine("\n---- РЕЖИМ АДМИНИСТРАТОРА ----");
            Console.WriteLine("1 - Пополнить товары");
            Console.WriteLine("2 - Посмотреть  и получить выручку");
            Console.Write("Выберите действие: ");

            string adminChoice = Console.ReadLine();

            if (adminChoice == "1")
            {
                ShowProducts();
                Console.Write("Выберите номер товара для пополнения: ");

                if (int.TryParse(Console.ReadLine(), out int productNumber) && productNumber > 0 && productNumber <= products.Count)
                {
                    Console.Write("Сколько товара добавить: ");
                    if (int.TryParse(Console.ReadLine(), out int amount))
                    {
                        products[productNumber - 1].Count += amount;
                        Console.WriteLine("Товары пополнены!");
                    }
                }
            }
            else if (adminChoice == "2")
            {
                Console.WriteLine($"\nОбщая выручка: {totalRevenue} руб.");
                if (totalRevenue > 0)
                {
                    Console.WriteLine($"Выдано {totalRevenue} руб. выручки.");
                    totalRevenue = 0;
                }
                else
                {
                    Console.WriteLine("Выручки нет");
                }
                ;
            }
        }
        else
        {
            Console.WriteLine("Неверный пароль.");
        }
    }
}

class Product
{
    public string Name;
    public int Price;
    public int Count;

    public Product(string name, int price, int count)
    {
        Name = name;
        Price = price;
        Count = count;
    }
}