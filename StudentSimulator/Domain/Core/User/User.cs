using System;
using StudentSimulator.Domain.Core.User.Characteristics;
namespace StudentSimulator.Domain.Core.User
{
    public class User
    {
        public int Id = 7;
        public string Name {get; set;}
        public string Gender {get; set;}
        public double Weight {get; set;}
        public double Height {get; set;}
        public Characteristic Health {get;} = new(100, 100, 0);
        public Characteristic Money {get;} = new(300, double.MaxValue, 0);
        public Characteristic Mood {get;} = new(100, 100, 0);
        public Characteristic WaterLevel {get;} = new(2, 2, 0);
        public Characteristic EatLevel {get;} = new(2, 2, 0);
        public Characteristic Stamina {get;} = new(100, 100, 0);

        public User(string name, string gender, double weight, double height)
        {
            Name = name;
            Gender = gender;
            Weight = weight;
            Height = height;
        }
    }
}