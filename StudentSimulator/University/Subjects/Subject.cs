namespace StudentSimulator.University.Subjects
{
    public class Subject
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public int Score {get; set;}

        public Subject(int id, string name, int score)
        {
            Id = id;
            Name = name;
            Score = score;
        }
    }
}