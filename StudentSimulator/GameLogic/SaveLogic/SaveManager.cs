using System.Text.Json;
using System.Text.Json.Serialization;
using StudentSimulator.Data.PayloadData;

namespace StudentSimulator.GameLogic.SaveLogic;

public class SaveManager
{
    private readonly string _savePath = "../../Data/Save.json";
    
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() },
    };

    public void Save(GameState state)
    {
        try
        {
            string json = JsonSerializer.Serialize(state, _options);
            File.WriteAllText(_savePath, json);
            Console.WriteLine("\nАвтозбереження завершено успішно.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nНе вдалося зберегти гру: {ex.Message}");
        }
    }

    public GameState? Load()
    {
        if (!File.Exists(_savePath))
        {
            return null;
        }

        try
        {
            string json = File.ReadAllText(_savePath);
            return JsonSerializer.Deserialize<GameState>(json, _options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nФайл збереження пошкоджено: {ex.Message}");
            return null;
        }
    }

    public bool HasSave() => File.Exists(_savePath);
}