namespace StudentSimulator.Works.Consultant;

public record ConsultingSession
{
    public int Id {get; set;}
    public string ClientName {get; set;}
    public ClientType ClientType {get; set;}
    public int Complexity {get; set;}
    public double MinProfit {get; set;}
    public int Duration {get; set;}
}