using System.Text.Json;
using System.Text.Json.Nodes;
using StudentSimulator.Data.PayloadData;

namespace StudentSimulator.University.Lectures
{
    public static class Lecture
    {
        public static void PrintLecture(string name, int num)
        {
            if(num < 1 || num > 10)
            {
                throw new ArgumentException("There is no lecture with that number!");
            } 

            LecturesPayload lecture = LoadLecture(name, num);
            Console.WriteLine(lecture.Text);
        }

        private static LecturesPayload LoadLecture(string name, int num)
        {
            string text = File.ReadAllText($"Data/LecturesData.json");
            List<LecturesPayload> lectures = JsonSerializer.Deserialize<List<LecturesPayload>>(text, new JsonSerializerOptions {WriteIndented = true});
            
            for(int i = 0; i < lectures.Count; i++)
            {
                if(lectures[i].Name == name && lectures[i].Number == num)
                {
                    return lectures[i];
                }
            }

            throw new ArgumentException("Load lecture is impossible!");
        }
    }
}