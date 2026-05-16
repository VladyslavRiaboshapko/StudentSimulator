using System;
using StudentSimulator.Domain.Core.User.Characteristics;
using StudentSimulator.Domain.Core.UserInventory;
namespace StudentSimulator.Domain.Core.User
{
    public class User
    {
        public int Id {get;} = 7;
        public string Name {get; set;}
        public string Gender {get; set;}
        public double Weight {get; set;}
        public double Height {get; set;}
        public Characteristic Health {get; set;} = new(100, 100, 0);
        public Characteristic Money {get; set;} = new(300, double.MaxValue, 0);
        public Characteristic Mood {get; set;} = new(100, 100, 0);
        public Characteristic WaterLevel {get; set;} = new(2, 2, 0);
        public Characteristic EatLevel {get; set;} = new(2, 2, 0);
        public Characteristic Stamina {get; set;} = new(100, 100, 0);
        public Inventory UserInventory {get; set;} = new(5);

        public User(string name, string gender, double weight, double height)
        {
            Name = name;
            Gender = gender;
            Weight = weight;
            Height = height;
        }
    }
}