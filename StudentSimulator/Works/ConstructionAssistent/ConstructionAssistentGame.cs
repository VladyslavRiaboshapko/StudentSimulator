using StudentSimulator.Data.PayloadData;

namespace StudentSimulator.Works.ConstructionAssistent;
public static class ConstructionAssistantGame
{
    public static bool Play(ConstructionSession session)
    {
        Console.WriteLine($"--- Робота: {session.TaskName} ---");
        Console.WriteLine($"Вам потрібно забити {session.NailsToHit} цвяхів!");
        
        int hits = 0;
        Random rng = new Random();

        for (int i = 1; i <= session.NailsToHit; i++)
        {
            
            int waitTime = rng.Next(500, 2000);
            Thread.Sleep(waitTime);

            Console.WriteLine("\nЗАРАЗ! (Тисни Enter!)");
            
            
            var startTime = DateTime.Now;
            Console.ReadLine();
            var reactionTime = (DateTime.Now - startTime).TotalMilliseconds;

            int allowedTime = 1000 - (session.SpeedMs * 50); 
            
            if (reactionTime <= allowedTime)
            {
                hits++;
                Console.WriteLine($"Влучив! ({reactionTime:F0} мс)");
            }
            else
            {
                Console.WriteLine($"Промах! Занадто повільно ({reactionTime:F0} мс)");
            }
        }

        return hits >= (session.NailsToHit * 0.7);
    }
}