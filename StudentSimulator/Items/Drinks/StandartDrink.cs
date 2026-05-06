namespace StudentSimulator.Items.Drinks;

public class StandartDrink : BaseDrink
{
    public StandartDrink(int id, string name, int maxStack, double liters) : base(id, name, maxStack, liters)
    {
        Id = id;
        Name = name;
        MaxStack = maxStack;
        Liters = liters;
    }
}