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
    public string ClientType;
    public string ProblemDescription;
    public string[] Options; 
    public string CorrectAnswer;
}