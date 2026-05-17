using System.Text.Json;
using System.Text.Encodings.Web;
using StudentSimulator.Data.PayloadData;

namespace StudentSimulator.University.Exams
{
    public static class Exam
    {
        public static void RunExam(string name)
        {
            int correctAnswers = 0;
            ExamsPayload exam = GetRequiredExam(name);

            for(int i = 0; i < exam.Questions.Count; i++)
            {
                Console.WriteLine(exam.Questions[i].Text);

                foreach(string j in exam.Questions[i].Options)
                {
                    Console.WriteLine(j);
                }

                string userInput = Console.ReadLine();
                
                if(userInput == exam.Questions[i].NumOfCorrectOption)
                {
                    correctAnswers++;
                }
            }

            if(ExamIsPassed(exam, correctAnswers))
            {
                exam.IsPassed = true;
                UpdateExamData(exam);
            }
        }

        private static void UpdateExamData(ExamsPayload examsData)
        {
            List<ExamsPayload> exams = LoadExams();

            for(int i = 0; i < exams.Count; i++)
            {
                if (exams[i].SubjectName == examsData.SubjectName)
                {
                    exams[i] = examsData;
                }
            }

            string text = JsonSerializer.Serialize<List<ExamsPayload>>(exams, new JsonSerializerOptions {WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
            File.WriteAllText("Data/ExamsData.json", text);
        }

        private static bool ExamIsPassed(ExamsPayload examsData, int correctAnswers)
        {
            if(correctAnswers < 1)
            {
                throw new ArgumentException("Некоректна кількість правильних відповідей!");
            }

            if(examsData.Questions.Count == correctAnswers)
            {
                return true;
            }

            return false;
        }

        private static ExamsPayload GetRequiredExam(string name)
        {
            List<ExamsPayload> exams = LoadExams();

            for(int i = 0; i < exams.Count; i++)
            {
                if(exams[i].SubjectName == name)
                {
                    return exams[i];
                }
            }

            throw new ArgumentException("Такого іспиту не існує!");
        }
        
        private static List<ExamsPayload> LoadExams()
        {
            string text = File.ReadAllText("Data/ExamsData.json");
            List<ExamsPayload> exams = JsonSerializer.Deserialize<List<ExamsPayload>>(text, new JsonSerializerOptions {WriteIndented = true});

            return exams;

            throw new ArgumentException("Неможливо завантажити іспит!");
        }
    }
}