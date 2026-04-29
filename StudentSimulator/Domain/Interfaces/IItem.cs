namespace StudentSimulator.Domain.Interfaces;

public interface IItem
{
    public int Id {get;}
    public string Name {get; set;}
    public int MaxStack {get; set;}
}