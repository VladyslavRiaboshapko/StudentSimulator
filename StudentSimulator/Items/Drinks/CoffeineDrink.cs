namespace StudentSimulator.Items.Drinks;

public class CoffeineDrink : BaseDrink
{
    public double Stamina {get; set;}

    public CoffeineDrink(int id, string name, int maxStack, double liters, double stamina) : base(id, name, maxStack, liters)
    {
        Id = id;
        Name = name;
        MaxStack = maxStack;
        Liters = liters;
        Stamina = stamina;
    }
}