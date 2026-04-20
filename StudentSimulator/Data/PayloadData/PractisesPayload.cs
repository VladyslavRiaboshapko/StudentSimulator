namespace StudentSimulator.Data.PayloadData
{
    public class PractisesPayload
    {
        public string Name {get; set;}
        public int Number {get; set;}

        public List<Question> Questions {get; set;}
        public List<string> InterectiveArgs {get; set;}
        public string Instructions {get; set;}
        public bool IsPassed {get; set;}

        public PractisesPayload(string name, int number, List<Question> questions, List<string> interectiveArgs, string instructions, bool isPassed = false)
        {
            Name = name;
            Number = number;
            Questions = questions;
            InterectiveArgs = interectiveArgs;
            Instructions = instructions;
            IsPassed = isPassed;
        }
    }
}