namespace StudentSimulator.Works.ConstructionAssistent;

public static class ConstructionAssistentRewardCalculator
{
    public static Reward Calculate(ConstructionTask task)
    {
        double money = task.Duration * task.DifficultyLevel * JobMultipliers.ConstructionAssistent;
        double stamina = task.Duration * task.DifficultyLevel * JobMultipliers.ConstructionAssistentStamina;
        double mood = task.Duration * JobMultipliers.MoodPerDuration + task.DifficultyLevel * task.Duration;

        return new Reward(money, stamina, mood);
    }
}