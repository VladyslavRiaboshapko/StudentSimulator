using StudentSimulator.Data.PayloadData;

namespace StudentSimulator.Works.Loader;
public static class LoaderGame
{
    public static bool Play(LoaderSession session)
    {
        int leftSide = 0;
        int rightSide = 0;

        Console.WriteLine($"--- Робота для {session.ClientName} ---");
        Console.WriteLine("Розподіліть вантаж, щоб уникнути перекосу!");

        foreach (var weight in session.Items)
        {
            Console.WriteLine($"\nКоробка вагою: {weight} кг");
            Console.WriteLine($"Поточний стан: [Ліво: {leftSide}] | [Право: {rightSide}]");
            Console.Write("Куди кладемо? (L/R): ");
            
            string choice = Console.ReadLine()?.ToUpper();
            if (choice == "L") leftSide += weight;
            else rightSide += weight;
        }

        int difference = Math.Abs(leftSide - rightSide);
        double imbalancePercent = (double)difference / (leftSide + rightSide) * 100;

        Console.WriteLine($"\nФінальний перекіс: {imbalancePercent:F2}%");

        if (imbalancePercent <= session.MaxImbalance)
        {
            Console.WriteLine("Вантажівка стабільна! Гарна робота.");
            return true;
        }
        
        Console.WriteLine("Вантажівка перекинулася на повороті...");
        return false;
    }
}