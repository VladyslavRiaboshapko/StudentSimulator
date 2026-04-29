namespace StudentSimulator.Items.Drinks;

public class StandartDrink : BaseDrink
{
    public int Id {get;}
    public string Name {get; set;}
    public int MaxStack {get; set;}
    public double Liters {get; set;}

    public StandartDrink(int id, string name, int maxStack, double liters) : base(id, name, maxStack, liters)
    {
        Id = id;
        Name = name;
        MaxStack = maxStack;
        Liters = liters;
    }
}