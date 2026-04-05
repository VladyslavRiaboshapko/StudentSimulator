namespace StudentSimulator.University.Practises
{
    public class PractiseData
    {
        public string Name {get; set;}
        public int Number {get; set;}

        public List<Question> Questions {get; set;}
        public string InterectiveType {get; set;}
        public string Instructions {get; set;}
        public bool IsPassed {get; set;}

        public PractiseData(string name, int number, List<Question> questions, string interectiveType, string instructions, bool isPassed = false)
        {
            Name = name;
            Number = number;
            Questions = questions;
            InterectiveType = interectiveType;
            Instructions = instructions;
            IsPassed = isPassed;
        }
    }
}