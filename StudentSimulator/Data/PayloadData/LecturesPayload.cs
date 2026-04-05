namespace StudentSimulator.Data.PayloadData
{
    public class LecturesPayload
    {
        public string Name {get; set;}
        public int Number {get; set;}
        public string Text {get; set;}

        public LecturesPayload(string name, int number, string text)
        {
            Name = name;
            Number = number;
            Text = text;
        }
    }
}