using StudentSimulator.Data.PayloadData;
using StudentSimulator.GameLogic.SaveLogic;
using StudentSimulator.Works;
using StudentSimulator.Exceptions;
using StudentSimulator.Shops;
using StudentSimulator.Works.Repo;
using StudentSimulator.Domain.Interfaces;
using StudentSimulator.Items.Drinks;
using StudentSimulator.Items.Foods;
using StudentSimulator.University.Practises;
using StudentSimulator.University.Exams;
using StudentSimulator.University.Lectures;

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
        if (input == "0" || input == "/back")
        {
            return;
        }

        if (int.TryParse(input, out int itemId))
        {
            IItem itemToBuy = null;
            int price = 0;
            bool itemFound = false;

           
            foreach (var pair in _supermarket.ProductRange)
            {
                if (pair.Key.Id == itemId)
                {
                    itemToBuy = pair.Key;
                    price = pair.Value;
                    itemFound = true;
                    break; 
                }
            }

            if (itemFound)
            {
                if (_state.Player.Money.Value >= price)
                {
                    if (_state.Player.Money.DecreaseValue(price))
                    {
                        _state.Player.UserInventory.AddItem(itemToBuy, 1);
                        Console.WriteLine($"\n[Успіх] Ви купили {itemToBuy.Name} за {price}грн!");
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
            AttendLecture();
            break;
            case "2": 
            StartPractise(); 
            break;
            case "3": 
            StartExam(); 
            break;
        }
    }

    private void StartPractise()
    {
        Console.WriteLine("\nОберіть назву предмету із списку та номер практичної від 1 до 10: ");
        Console.WriteLine("1. Алгоритми та структури даних");
        Console.WriteLine("2. Англійська");
        Console.WriteLine("3. Математика");
        Console.WriteLine("4. Програмування");
        Console.WriteLine("5. Фізика");
        Console.WriteLine("6. Хімія");
        Console.WriteLine("7. Переглянути список практик");
        Console.WriteLine("0. Назад");

        Console.Write("\nОберіть дію або номер предмету: ");

        string input = GetInput(); 

        if (input == "0" || input == "/back")
        {
            return;
        }
        
        if (input == "7")
        {
            ShowPractisesList();
            return;
        }

        if (_state.Player.Stamina.Value < 20)
        {
            Console.WriteLine("\n[Помилка] Ви занадто втомлені! Треба відпочити або випити кави.");
            Console.ReadKey();
            return;
        }

        Console.Write("\nНомер практичної: ");
        int practiseNumber = Convert.ToInt32(Console.ReadLine());

        string subjectName = "";
        switch (input)
        {
            case "1": 
            subjectName = "ADS"; 
            break;
            case "2": 
            subjectName = "English"; 
            break;
            case "3": 
            subjectName = "Math"; 
            break;
            case "4": 
            subjectName = "Programming"; 
            break;
            case "5": 
            subjectName = "Physic"; 
            break;
            case "6": 
            subjectName = "Chemistry"; 
            break;
            default:
                Console.WriteLine("Невірна назва предмету!");
                Console.ReadKey();
                return;
        }

        PractiseEngine.RunPractise(subjectName, practiseNumber);
    }

    private void StartExam()
    {
        Console.WriteLine("\nОберіть назву предмету із списку: ");
        Console.WriteLine("1. Алгоритми та структури даних");
        Console.WriteLine("2. Англійська");
        Console.WriteLine("3. Математика");
        Console.WriteLine("4. Програмування");
        Console.WriteLine("5. Фізика");
        Console.WriteLine("6. Хімія");

        string input = Console.ReadLine();

        string subjectName = "";

        switch(input)
        {
            case "1":
            subjectName = "ADS";
            break;
            case "2":
            subjectName = "English";
            break;
            case "3":
            subjectName = "Math";
            break;
            case "4":
            subjectName = "Programming";
            break;
            case "5":
            subjectName = "Physic";
            break;
            case "6":
            subjectName = "Chemistry";
            break;
            default:
            Console.WriteLine("Невірне значення!");
            Console.ReadKey();
            return;
        }

        List<PractisesPayload> practises = PractiseEngine.LoadPractisesData();

        int c = 0;

        for(int i = 0; i < practises.Count; i++)
        {
            if(practises[i].Name == subjectName && practises[i].IsPassed == true)
            {
                c++;
            } 
        }

        if(c == 10)
        {
            Exam.RunExam(subjectName);
        }
        else
        {
            Console.WriteLine($"\n[ВІДМОВЛЕНО В ДОПУСКУ] Ви здали лише {c} із 10 практик.");
            Console.WriteLine("Вам залишилося пройти такі практичні роботи з цього предмету:");
            for (int i = 0; i < practises.Count; i++)
            {
                if (practises[i].Name == subjectName && practises[i].IsPassed == false)
                {
                    Console.WriteLine($" ❌ Практична робота №{practises[i].Number}");
                }
            }
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }

    private void AttendLecture()
    {
        Console.WriteLine("\nОберіть назву предмету із списку та номер лекції від 1 до 10: ");
        Console.WriteLine("1. Алгоритми та структури даних");
        Console.WriteLine("2. Англійська");
        Console.WriteLine("3. Математика");
        Console.WriteLine("4. Програмування");
        Console.WriteLine("5. Фізика");
        Console.WriteLine("6. Хімія");

        Console.Write("\nНомер предмету: ");
        string input = GetInput();
        if (input == "0" || input == "/back") return;

        if (_state.Player.Stamina.Value < 10)
        {
            Console.WriteLine("\n[Помилка] Ви занадто виснажені, щоб слухати лекцію. Спіть або їжте!");
            Console.ReadKey();
            return;
        }

        Console.Write("Номер лекції (1-10): ");
        string numInput = GetInput();
        if (numInput == "/back") return;
        int lectureNumber = Convert.ToInt32(numInput);

        string subjectName = "";
        switch (input)
        {
            case "1": subjectName = "ADS"; 
            break;
            case "2": subjectName = "English"; 
            break;
            case "3": subjectName = "Math"; 
            break;
            case "4": subjectName = "Programming"; 
            break;
            case "5": subjectName = "Physic"; 
            break;
            case "6": subjectName = "Chemistry"; 
            break;
            default:
                Console.WriteLine("Невірна назва предмету або номер!");
                Console.ReadKey();
                return;
        }

        _state.Player.Stamina.DecreaseValue(10);
        _state.Player.EatLevel.DecreaseValue(0.2);
        _state.Player.WaterLevel.DecreaseValue(0.2);

        Lecture.PrintLecture(subjectName, lectureNumber);
        Console.WriteLine("\nНатисніть будь-яку клавішу для повернення до меню дій...");
        Console.ReadKey();
    }

    private void ShowPractisesList()
    {
        Console.Clear();
        Console.WriteLine("=== СПИСОК ВСІХ ПРАКТИЧНИХ РОБІТ ===");
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine($"| {"Предмет",-15} | {"Номер",-5} | {"Статус",-10} |");
        Console.WriteLine("----------------------------------------------------");

        List<PractisesPayload> practises = PractiseEngine.LoadPractisesData();
        
        for (int i = 0; i < practises.Count; i++)
        {
            string status = practises[i].IsPassed ? "Пройдено" : "❌ НІ";
            Console.WriteLine($"| {practises[i].Name,-15} | {practises[i].Number,-5} | {status,-10} |");
        }
        Console.WriteLine("----------------------------------------------------");
        Console.WriteLine("\nНатисніть будь-яку клавішу для повернення...");
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
            if (slots[i].Amount > 0 && slots[i].Item != null)
            {
                Console.WriteLine($"{i + 1}. {slots[i].Item.Name} (Кількість: {slots[i].Amount})");
            }
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
        if (item is Food food)
        {
            _state.Player.EatLevel.IncreaseValue(food.Kilograms); 
            _state.Player.Health.IncreaseValue(5);   
            Console.WriteLine($"\n[ІНВЕНТАР] Ви з'їли {food.Name}. Рівень голоду покращено на {food.Kilograms}");
        }
    
        else if (item is CoffeineDrink coffee)
        {
            _state.Player.WaterLevel.IncreaseValue(coffee.Liters);   
            _state.Player.Stamina.IncreaseValue(coffee.Stamina); 
            _state.Player.Mood.IncreaseValue(10);
            Console.WriteLine($"\n[ІНВЕНТАР] Ви випили {coffee.Name}. Спрагу вгамовано! Стаміна +{coffee.Stamina}, Настрій покращено.");
        }

        else if (item is BaseDrink drink) 
        {
            _state.Player.WaterLevel.IncreaseValue(drink.Liters); 
            Console.WriteLine($"\n[ІНВЕНТАР] Ви випили {drink.Name}. Спрагу успішно вгамовано! (Об'єм: {drink.Liters}л)");
        }
    }
}