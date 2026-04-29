using StudentSimulator.Domain.Interfaces;

namespace StudentSimulator.Items.Foods;

public class Food : IItem
{
    public int Id {get;}
    public string Name {get; set;}
    public int MaxStack {get; set;}
    public double Kilograms {get; set;}

    public Food(int id, string name, int maxStack, double kilograms)
    {
        Id = id;
        Name = name;
        MaxStack = maxStack;
        Kilograms = kilograms;
    }
}