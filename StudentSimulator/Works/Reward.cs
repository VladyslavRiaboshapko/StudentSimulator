namespace StudentSimulator.Works;
public struct Reward
{
    public double Money {get; set;}
    public double Stamina {get; set;}
    public double Mood {get; set;}

    public Reward(double money, double stamina, double mood)
    {
        Money = money;
        Stamina = stamina;
        Mood = mood;
    }
}