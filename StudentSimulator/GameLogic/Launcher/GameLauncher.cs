using StudentSimulator.GameLogic.SaveLogic;
using StudentSimulator.Data.PayloadData;
using StudentSimulator.Domain.Core.User;
namespace StudentSimulator.GameLogic.Launcher;

public class GameLauncher
{
    private readonly SaveManager _saveManager = new();

    public void Start()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== STUDENT SIMULATOR 2026 ===");
            Console.WriteLine("1. Продовжити гру");
            Console.WriteLine("2. Нова гра");
            Console.WriteLine("3. Інструкція");
            Console.WriteLine("4. Вихід");
            Console.Write("\nОберіть опцію: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": 
                ContinueGame(); 
                break;

                case "2": 
                StartNewGame(); 
                break;

                case "3": 
                ShowManual(); 
                break;

                case "4": 
                ExitProgram(); 
                return;

                default: 
                Console.WriteLine("Невірний ввід!"); 
                break;
            }
        }
    }

    private void ContinueGame()
    {
        var state = _saveManager.Load();
        if (state != null) 
        {
            GameEngine engine = GameServiceProvider.CreateEngine(state, _saveManager);
            engine.Play();
        }
        else 
        {
            Console.WriteLine("Файл збереження не знайдено! Почніть нову гру.");
            Console.ReadKey();
        }
    }

    private void StartNewGame()
    {
        Console.WriteLine("Ви впевнені? Весь прогрес буде видалено! (y/n)");

        if (Console.ReadLine()?.ToLower() != "y")
        {
            return;
        }
        
        string examsTemplate = "Data/Templates/ExamsData.json";
        string practiseTemplate = "Data/Templates/PractisesData.json";

        string examsActive = "Data/ExamsData.json";
        string practiseActive = "Data/PractisesData.json";

        
        if (File.Exists(examsTemplate) && File.Exists(practiseTemplate))
        {
            File.Copy(examsTemplate, examsActive, true);
            File.Copy(practiseTemplate, practiseActive, true);
            
            Console.WriteLine("[Система] Навчальний прогрес успішно скинуто до початкового стану.");
        }
        else
        {
            Console.WriteLine("[Попередження] Не знайдено файли шаблонів у папці Templates! Гра почнеться зі старими практиками.");
            Console.ReadKey();
        }
        

        User newUser = new User("Студент", "Ч", 75, 180);
        newUser.UserInventory.InitializeEmptySlots(5);
        GameState newState = new GameState(newUser, 1);
        _saveManager.Save(newState);

        GameEngine engine = GameServiceProvider.CreateEngine(newState, _saveManager);
        engine.Play();
    }

    private void ShowManual()
    {
        Console.Clear();
        Console.WriteLine("=== ІНСТРУКЦІЯ ===");
        Console.WriteLine("* Керування: вводьте цифри для вибору пунктів меню.");
        Console.WriteLine("* Команди в будь-який момент:");
        Console.WriteLine("  /back - повернутися на крок назад");
        Console.WriteLine("  /main - вийти в головне меню (зі збереженням)");
        Console.WriteLine("  /exit - закрити гру (зі збереженням)");
        Console.WriteLine("\n* Твоя мета: Прожити семестр, не померти від голоду та скласти іспити!");
        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    private void ExitProgram(GameState? currentState = null)
    {
        Console.Clear();
        Console.WriteLine("Зберігаємо прогрес та виходимо...");

        
        if (currentState != null)
        {
            _saveManager.Save(currentState);
        }

        Console.WriteLine("До зустрічі!");
        
        
        Thread.Sleep(1000); 
        
        Environment.Exit(0);
    }
}