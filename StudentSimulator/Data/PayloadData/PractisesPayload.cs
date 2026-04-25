namespace StudentSimulator.Data.PayloadData
{
    public class PractisesPayload
    {
        public string Name {get; set;}
        public int Number {get; set;}

        public List<Question> Questions {get; set;}
        public List<string> InteractiveArgs {get; set;}
        public string Instructions {get; set;}
        public bool IsPassed {get; set;}

        public PractisesPayload(string name, int number, List<Question> questions, List<string> interactiveArgs, string instructions, bool isPassed = false)
        {
            Name = name;
            Number = number;
            Questions = questions;
            InteractiveArgs = interactiveArgs;
            Instructions = instructions;
            IsPassed = isPassed;
        }
    }
}