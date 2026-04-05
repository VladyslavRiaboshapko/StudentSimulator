namespace StudentSimulator.University.Exams
{
    public class Exam
    {
        public string SubjectName {get; set;}
        public bool IsPassed {get; set;}

        public Exam(string subjectName, bool isPassed)
        {
            SubjectName = subjectName;
            IsPassed = isPassed;
        }
    }
}