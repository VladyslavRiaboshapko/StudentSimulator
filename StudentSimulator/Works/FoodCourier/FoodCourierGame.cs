using StudentSimulator.Data.PayloadData;
namespace StudentSimulator.Works.FoodCourier;
public static class FoodCourierGame
{
    private static Random _rng = new Random();

    public static bool Play(CourierSession session)
    {
        Console.WriteLine($"--- Доставка для {session.ClientName} ---");
        Console.WriteLine($"Замовлення: {string.Join(", ", session.OrderItems)}");
        
        int successPoints = 0;

        for (int i = 1; i <= session.HurdlesCount; i++)
        {
            Console.WriteLine($"\nЕтап {i}:");
            
            int situation = _rng.Next(1, 3); 
            
            if (situation == 1) 
            {
                Console.WriteLine("Попереду сильний затор! Що робимо?");
                Console.WriteLine("1. Чекати (Втрата часу)");
                Console.WriteLine("2. Об'їхати дворами (Ризик заблукати)");
                
                string choice = Console.ReadLine();
                if (choice == "2") successPoints++; 
            }
            else 
            {
                Console.WriteLine("Шлях перегородив злий пес! Ваші дії?");
                Console.WriteLine("1. Пройти повз спокійно");
                Console.WriteLine("2. Почати бігти");
                
                string choice = Console.ReadLine();
                if (choice == "1") successPoints++;
            }
        }

        
        return successPoints >= (session.HurdlesCount / 2);
    }
}