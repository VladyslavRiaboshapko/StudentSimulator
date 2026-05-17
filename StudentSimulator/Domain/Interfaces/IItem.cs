using System.Text.Json.Serialization;
using StudentSimulator.Items.Foods;
using StudentSimulator.Items.Drinks;

namespace StudentSimulator.Domain.Interfaces;

[JsonDerivedType(typeof(Food), typeDiscriminator: "food")]
[JsonDerivedType(typeof(StandartDrink), typeDiscriminator: "drink")]
[JsonDerivedType(typeof(CoffeineDrink), typeDiscriminator: "coffee")]
public interface IItem
{
    public int Id {get;}
    public string Name {get; set;}
    public int MaxStack {get; set;}
}