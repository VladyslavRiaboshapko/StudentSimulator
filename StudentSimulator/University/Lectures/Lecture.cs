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

            string text = File.ReadAllText($"University/Lectures/LectureList/{name}Lectures/{name}Lecture{num}.bin");
            Console.WriteLine(text);
        }
    }
}