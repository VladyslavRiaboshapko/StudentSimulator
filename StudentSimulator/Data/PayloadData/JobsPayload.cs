using System.Text.Json.Serialization;
using StudentSimulator.Works.Consultant;

namespace StudentSimulator.Data.PayloadData;
public struct BaristaSession
{
    public string TargetDrink;
    public string[] ShuffledActions;
    public string[] CorrectOrder;
}

public struct LoaderSession
{
    public string ClientName;
    public int[] Items;     
    public double MaxImbalance; 
}

public struct CourierSession
{
    public string ClientName;
    public int Distance;
    public string[] OrderItems;
    public int HurdlesCount; 
}

public struct ConsultantSession
{
    public string ClientName;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ClientType ClientType;
    public string ProblemDescription;
    public string[] Options; 
    public string CorrectAnswer;
}

public struct ConstructionSession
{
    public string TaskName;
    public int NailsToHit;
    public int SpeedMs; 
}