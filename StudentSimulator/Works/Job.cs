namespace StudentSimulator.Works;

public class Job<TTask>
{
    public List<TTask> Tasks {get; set;}
    public RewardCalculator<TTask> Calculator {get; set;}
    public double BaseStaminaCost {get; set;}
    public JobTypes Type {get; set;}
}