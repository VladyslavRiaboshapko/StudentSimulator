using StudentSimulator.Domain.Interfaces;

namespace StudentSimulator.Items.Drinks;
public abstract class BaseDrink : IItem
{
    public int Id {get;}
    public string Name {get; set;}
    public int MaxStack {get; set;}
    public double Liters {get; set;}

    public BaseDrink(int id, string name, int maxStack, double liters)
    {
        Id = id;
        Name = name;
        MaxStack = maxStack;
        Liters = liters;
    }
}