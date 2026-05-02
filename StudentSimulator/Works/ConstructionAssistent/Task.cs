namespace StudentSimulator.Works.ConstructionAssistent;

public record Task
{
    public int Id {get; set;}
    public string Name {get; set;}
    public int Duration {get; set;}
    public int DifficultyLevel {get; set;}
}