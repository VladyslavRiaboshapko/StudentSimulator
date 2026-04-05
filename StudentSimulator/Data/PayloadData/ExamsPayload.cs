namespace StudentSimulator.Data.PayloadData
{
    public class ExamsPayload
    {
        public string SubjectName {get; set;}
        public List<Question> Questions {get; set;}
        public bool IsPassed {get; set;} 

        public ExamsPayload(string subjectName, List<Question> questions, bool isPassed)
        {
            SubjectName = subjectName;
            Questions = questions;
            IsPassed = isPassed;
        }
    }
}