using StudentSimulator.Data.PayloadData;
namespace StudentSimulator.Works.Consultant;
public static class ConsultantGame
{
    public static bool Play(ConsultantSession session)
    {
        Console.WriteLine($"--- Консультація: {session.ClientName} ({session.ClientType}) ---");
        Console.WriteLine($"Проблема: {session.ProblemDescription}");
        Console.WriteLine("\nВаріанти вирішення:");

        for (int i = 0; i < session.Options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {session.Options[i]}");
        }

        Console.Write("\nВаш діагноз (номер): ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= session.Options.Length)
        {
            if (session.Options[choice - 1] == session.CorrectAnswer)
            {
                Console.WriteLine("Клієнт вражений вашим професіоналізмом!");
                return true;
            }
        }

        Console.WriteLine("Клієнт незадоволений порадою. Ви втратили репутацію.");
        return false;
    }
}