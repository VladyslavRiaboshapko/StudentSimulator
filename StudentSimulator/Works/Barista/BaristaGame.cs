using StudentSimulator.Data.PayloadData;

namespace StudentSimulator.Works.Barista
{
    public class BaristaGame
    {
        public static bool Play(BaristaSession payload)
        {
            string[] userOrder = new string[payload.CorrectOrder.Length];
            
            Console.WriteLine($"--- Готуємо: {payload.TargetDrink} ---");
            Console.WriteLine("Оберіть правильний порядок дій:");
            
            for (int i = 0; i < payload.ShuffledActions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {payload.ShuffledActions[i]}");
            }

            for (int i = 0; i < userOrder.Length; i++)
            {
                Console.Write($"Крок {i + 1}: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= payload.ShuffledActions.Length)
                {
                    userOrder[i] = payload.ShuffledActions[choice - 1];
                }
                else
                {
                    Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                    i--;
                }
            }

            for (int i = 0; i < payload.CorrectOrder.Length; i++)
            {
                if (userOrder[i] != payload.CorrectOrder[i])
                {
                    Console.WriteLine("\nПомилка! Кава зіпсована.");
                    return false;
                }
            }

            Console.WriteLine("\nЧудова кава! Клієнт задоволений.");
            return true;
        }
    }
}