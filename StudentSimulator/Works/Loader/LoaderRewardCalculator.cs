namespace StudentSimulator.Works.Loader;
public static class LoaderRewardCalculator
{
    public static Reward Calculate(LoadTask task)
    {
        double money = task.TotalWeight / task.Members;
        double stamina = task.TotalWeight / task.Members * JobMultipliers.LoaderStamina;
        double mood = task.TotalWeight / task.Members * JobMultipliers.LoaderMood;

        return new Reward(money, stamina, mood);
    }
}