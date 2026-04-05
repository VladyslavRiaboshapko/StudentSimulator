namespace StudentSimulator.University.Practises
{
    public class Question
    {
        public string Text {get; set;}
        public string[] Options {get; set;}
        public string NumOfCorrectOption {get; set;}

        public Question(string text, string[] options, string numOfCorrectOption)
        {
            Text = text;
            Options = options;
            NumOfCorrectOption = numOfCorrectOption;
        }
    }
}