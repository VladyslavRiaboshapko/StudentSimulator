namespace StudentSimulator.Works.Consultant;

public static class SuccessRandomizer
{
    private static readonly Random _random = new();

    public static bool IsSuccess(double successChancePercent)
    {
        double roll = _random.NextDouble() * 100; 
        return roll <= successChancePercent;
    }
}