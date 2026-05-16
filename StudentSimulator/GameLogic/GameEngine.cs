using StudentSimulator.Data.PayloadData;
using StudentSimulator.GameLogic.SaveLogic;
using StudentSimulator.Works;
using StudentSimulator.Exceptions;
using StudentSimulator.Shops;
using StudentSimulator.Works.Repo;
using StudentSimulator.Domain.Interfaces;
using StudentSimulator.Items.Drinks;
using StudentSimulator.Items.Foods;

namespace StudentSimulator.GameLogic;

public class GameEngine
{
    private GameState _state;
    private readonly SaveManager _saveManager;
    private readonly JobManager _jobManager;
    private readonly Supermarket _supermarket;

    public GameEngine(GameState state, SaveManager saveManager, JobManager jobManager)
    {
        _state = state;
        _saveManager = saveManager;
        _jobManager = jobManager;
        _supermarket = SupermarketFactory.CreateSupermarket();
    }

    public void Play()
    {
        try
        {
            while (IsAlive())
            {
                Console.Clear();
                ShowStatus();
                Console.WriteLine("\n--- КУДИ ПІДЕМО? ---");
                Console.WriteLine("1. Навчання (Лекції/Практики)");
                Console.WriteLine("2. Підробіток (Робота)");
                Console.WriteLine("3. Магазин (Продукти)");
                Console.WriteLine("4. Інвентар");
                Console.WriteLine("5. Завершити день (Відпочинок та сейв)");
                Console.Write("\nОберіть дію або введіть команду (/exit, /main): ");

                string input = GetInput();

                switch (input)
                {
                    case "1": 
                    GoToUniversity(); 
                    break;
                    case "2": 
                    GoToWork(); 
                    break;
                    case "3": 
                    GoToShop(); 
                    break;
                    case "4": 
                    OpenInventory(); 
                    break;
                    case "5": 
                    FinishDay(); 
                    break;
                    default: 
                    Console.WriteLine("Невідома дія. Спробуйте ще раз."); 
                    break;
                }
            }
        }
        catch (NavigationException ex)
        {
            if (ex.Target == NavigationTarget.Main)
            {
                return;
            }
        }
    }

    private string GetInput()
    {
        string input = Console.ReadLine()?.ToLower().Trim();

        if (input == "/exit") 
        {
            _saveManager.Save(_state);
            Environment.Exit(0);
        }
        
        if (input == "/main") 
        {
            _saveManager.Save(_state);
            throw new NavigationException(NavigationTarget.Main);
        }

        return input;
    }

    private void ShowStatus()
    {
        var p = _state.Player;
        Console.WriteLine($"День: {_state.CurrentDay} | Студент: {p.Name}");
        Console.WriteLine($"Гроші: {p.Money.Value} | Стаміна: {p.Stamina.Value} | Настрій: {p.Mood.Value}");
        Console.WriteLine($"Голод: {p.EatLevel.Value} | Спрага: {p.WaterLevel.Value}");
    }

    private void GoToWork() => ShowJobMenu(); 
    private void GoToShop()
    {
        Console.Clear();
        _supermarket.ShowProducts();
        Console.WriteLine("\n[0] Повернутися назад");
        Console.Write("\nВведіть ID товару для покупки: ");

        string input = GetInput();
        if (input == "0" || input == "/back") return;

        if (int.TryParse(input, out int itemId))
        {
            var entry = _supermarket.ProductRange.FirstOrDefault(x => x.Key.Id == itemId);
            
            if (entry.Key != null)
            {
                IItem item = entry.Key;
                int price = entry.Value;

                if (_state.Player.Money.Value >= price)
                {
                    if (_state.Player.Money.DecreaseValue(price))
                    {
                        _state.Player.UserInventory.AddItem(item, 1);
                        Console.WriteLine($"\n[Успіх] Ви купили {item.Name}!");
                    }
                }
                else
                {
                    Console.WriteLine("\n[Помилка] Недостатньо коштів!");
                }
            }
            else
            {
                Console.WriteLine("\nТовар не знайдено.");
            }
        }
        Console.ReadKey();
    }

    private void FinishDay()
    {
        Console.WriteLine("\nВи вирішили відпочити. День завершується...");
        _state.CurrentDay++;
        
        _state.Player.Stamina.IncreaseValue(50);
        _state.Player.EatLevel.DecreaseValue(0.5);
        _state.Player.WaterLevel.DecreaseValue(0.5);

        _saveManager.Save(_state);
        Console.WriteLine("Прогрес збережено! Натисніть клавішу для початку нового дня...");
        Console.ReadKey();
    }

    private void ShowJobMenu()
    {
        Console.Clear();
        Console.WriteLine("--- ДОСТУПНІ ПІДРОБІТКИ ---");
        Console.WriteLine("1. Консультант");
        Console.WriteLine("2. Кур'єр");
        Console.WriteLine("3. Вантажник");
        Console.WriteLine("4. Бариста");
        Console.WriteLine("5. Помічник будівельника");
        Console.WriteLine("0. Назад");

        string choice = GetInput();
        switch (choice)
        {
            case "1": 
            _jobManager.StartConsultantJob(); 
            break;
            case "2": 
            _jobManager.StartFoodCourierJob();
            break;
            case "3":
            _jobManager.StartLoaderJob();
            break;
            case "4":
            _jobManager.StartBaristaJob();
            break;
            case "5":
            _jobManager.StartConstructionJob();
            break;
            case "0": 
            return;
        }
    }

    private bool IsAlive() => _state.Player.Health.Value > 0 && _state.Player.Mood.Value > 0;
    
    private void GoToUniversity()
    {
        Console.Clear();
        Console.WriteLine("=== УНІВЕРСИТЕТ ===");
        Console.WriteLine("1. Прослухати лекцію (-15 стаміна, +5 знання)");
        Console.WriteLine("2. Пройти практику (Міні-гра)");
        Console.WriteLine("3. Скласти іспит (Фінальний тест)");
        Console.WriteLine("0. Назад");

        string choice = GetInput();
        switch (choice)
        {
            case "1": 
            case "2": StartPractise(); break;
            case "3": StartExam(); break;
        }
    }

    private void AttendLecture()
    {
        if (_state.Player.Stamina.Value >= 15)
        {
            _state.Player.Stamina.DecreaseValue(15);
            _state.Player.Mood.DecreaseValue(5);
            
            Console.WriteLine("\nЛекція була нудною, але корисною. Знання отримано!");
        }
        else
        {
            Console.WriteLine("\nВи занадто втомлені для лекцій.");
        }
        Console.ReadKey();
    }
    private void OpenInventory()
    {
        Console.Clear();
        Console.WriteLine("=== ВАШ ІНВЕНТАР ===");
        
        var slots = _state.Player.UserInventory.Slots;
        if (slots.Count == 0)
        {
            Console.WriteLine("Ваша сумка порожня.");
            Console.ReadKey();
            return;
        }

        for (int i = 0; i < slots.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {slots[i].Item.Name} (Кількість: {slots[i].Amount})");
        }

        Console.WriteLine("\n[0] Назад | Оберіть номер предмета, щоб використати його:");
        string input = GetInput();

        if (int.TryParse(input, out int index) && index > 0 && index <= slots.Count)
        {
            var item = slots[index - 1].Item;
            UseItem(item);
            _state.Player.UserInventory.RemoveItemById(item.Id, 1);
        }
    }

    private void UseItem(IItem item)
    {
        if (item is CoffeineDrink coffee)
        {
            _state.Player.Stamina.IncreaseValue(coffee.Stamina);
            _state.Player.Mood.IncreaseValue(10);
            Console.WriteLine($"\nВи випили {coffee.Name}. Стаміна відновлена на {coffee.Stamina}!");
        }
        else if (item is Food food)
        {
            _state.Player.EatLevel.IncreaseValue(1.0);
            _state.Player.Health.IncreaseValue(5);
            Console.WriteLine($"\nВи з'їли {food.Name}. Рівень голоду покращено!");
        }
        Console.ReadKey();
    }
}