using System.Text.Json;
using StudentSimulator.Data.PayloadData;
using StudentSimulator.Works.FoodCourier;
using StudentSimulator.Works.Consultant;

namespace StudentSimulator.Works.Repo;

public class JobsRepository
{
    private JsonDocument _data;
    private Random _rng = new Random();

    public JobsRepository(string jsonPath)
    {
        string jsonString = File.ReadAllText(jsonPath);
        _data = JsonDocument.Parse(jsonString);
    }

    public T GetRandomTask<T>(string jobName)
    {
        var jobArray = _data.RootElement.GetProperty(jobName);
        int randomIndex = _rng.Next(jobArray.GetArrayLength());
        
        string taskJson = jobArray[randomIndex].GetRawText();
        return JsonSerializer.Deserialize<T>(taskJson);
    }

    public BaristaSession PrepareBaristaSession(int taskId)
    {
        var baristaList = _data.RootElement.GetProperty("Barista");
        JsonElement selectedElement = default;
        bool found = false;

        for (int i = 0; i < baristaList.GetArrayLength(); i++)
        {
            if (baristaList[i].GetProperty("Id").GetInt32() == taskId)
            {
                selectedElement = baristaList[i];
                found = true;
                break;
            }
        }

        if (!found)
        {
            throw new ArgumentException($"Task with Id {taskId} not found in Barista jobs!");
        }

        string targetDrink = selectedElement.GetProperty("Target").GetString();
        var actionsElement = selectedElement.GetProperty("Actions");
        string[] correctOrder = new string[actionsElement.GetArrayLength()];

        for (int i = 0; i < actionsElement.GetArrayLength(); i++)
        {
            correctOrder[i] = actionsElement[i].GetString();
        }

        string[] shuffled = new string[correctOrder.Length];
        Array.Copy(correctOrder, shuffled, correctOrder.Length);


        for (int i = shuffled.Length - 1; i > 0; i--)
        {
            int j = _rng.Next(i + 1);
            string temp = shuffled[i];
            shuffled[i] = shuffled[j];
            shuffled[j] = temp;
        }

        return new BaristaSession
        {
            TargetDrink = targetDrink,
            CorrectOrder = correctOrder,
            ShuffledActions = shuffled
        };
    }

    public CourierSession PrepareCourierSession(DeliveryOrder order)
    {
        return new CourierSession
        {
            ClientName = order.ClientName,
            Distance = order.Distance,
            OrderItems = order.Items.ToArray(),
            HurdlesCount = Math.Max(2, order.Distance / 500) 
        };
    }

    public ConsultantSession PrepareConsultingSession(ConsultingSession data, string problem, string correct)
    {
        string[] options = new string[] 
        { 
            correct, 
            "Просто ігнорувати проблему", 
            "Звільнити всіх працівників", 
            "Підняти ціни в 10 разів" 
        };

        
        Random rng = new Random();
        for (int i = options.Length - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            string temp = options[i];
            options[i] = options[j];
            options[j] = temp;
        }

        return new ConsultantSession
        {
            ClientName = data.ClientName,
            ClientType = data.ClientType.ToString(),
            ProblemDescription = problem,
            Options = options,
            CorrectAnswer = correct
        };
    }

    public int[] GetItemsForLoading(int taskId)
    {
        var loaderList = _data.RootElement.GetProperty("Loader");
        
        for (int i = 0; i < loaderList.GetArrayLength(); i++)
        {
            if (loaderList[i].GetProperty("Id").GetInt32() == taskId)
            {
                var itemsElement = loaderList[i].GetProperty("ItemsToLoad");
                int[] items = new int[itemsElement.GetArrayLength()];
                for (int j = 0; j < itemsElement.GetArrayLength(); j++)
                {
                    items[j] = itemsElement[j].GetInt32();
                }
                return items;
            }
        }
        return new int[0];
    }

   
    public string GetRawProperty(string jobName, int taskId, string propertyName)
    {
        var jobArray = _data.RootElement.GetProperty(jobName);

        for (int i = 0; i < jobArray.GetArrayLength(); i++)
        {
            if (jobArray[i].GetProperty("Id").GetInt32() == taskId)
            {
                if (jobArray[i].TryGetProperty(propertyName, out JsonElement property))
                {
                    return property.GetString();
                }
            }
        }

        return "Property not found";
    }
}