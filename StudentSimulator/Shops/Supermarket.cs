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
}