namespace StudentSimulator.Works.Barista;

public static class CourierRewardCalculator
{
    public static Reward Calculate(Order order)
    {
        double money = order.Clients * order.Duration * order.CafeLevel;
        double stamina = order.Clients * JobMultipliers.StaminaPerClients;
        double mood = order.Duration * JobMultipliers.MoodPerDuration - order.CafeLevel;

        return new Reward(money, stamina, mood);
    }
}