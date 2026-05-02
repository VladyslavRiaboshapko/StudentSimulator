namespace StudentSimulator.Works.Loader;

public record LoadTask
{
    public int Id {get; set;}
    public string ClientName {get; set;}
    public int TotalWeight {get; set;}
    public int Members {get; set;}
}