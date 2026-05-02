namespace StudentSimulator.Works.FoodCourier;

public static class CourierRewardCalculator
{
    public static Reward Calculate(DeliveryOrder order)
    {
        double money = order.Distance * JobMultipliers.MoneyPerDistance;
        double stamina = order.Distance * JobMultipliers.StaminaPerDistance;
        double mood = order.Distance * JobMultipliers.MoodPerDistance;

        return new Reward(money, stamina, mood);
    }
}