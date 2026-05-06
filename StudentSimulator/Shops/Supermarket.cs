using StudentSimulator.Domain.Interfaces;
using StudentSimulator.Items.Drinks;
using StudentSimulator.Items.Foods;

namespace StudentSimulator.Shops;

public class Supermarket
{
    public int Id {get;}
    public string Name {get; set;}
    public Dictionary<IItem, int> ProductRange {get; set;}

    public Supermarket(int id, string name, Dictionary<IItem, int> productRange)
    {
        Id = id;
        Name = name;
        ProductRange = productRange;
    }

    public void ShowProducts()
    {
        Console.WriteLine($"\n--- Вітаємо у {Name} ---");
        foreach (var product in ProductRange)
        {
            Console.WriteLine($"ID: {product.Key.Id} | {product.Key.Name} | Ціна: {product.Value}грн");
        }
    }
}