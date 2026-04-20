using System.Text.Json;
using System.Text.Encodings.Web;
using StudentSimulator.Data.PayloadData;
using StudentSimulator.University.Practises.InterectiveTypes;

namespace StudentSimulator.University.Practises
{
    public static class PractiseEngine
    {
        public static void RunPractise(string name, int num)
        {
            PractisesPayload practiseData = GetRequiredPractise(name, num);
            int correctAnswers = 0;

            for(int i = 0; i < practiseData.Questions.Count; i++)
            {
                Console.WriteLine(practiseData.Questions[i].Text);
                foreach(string j in practiseData.Questions[i].Options)
                {
                    Console.WriteLine(j);
                }
                
                string userInput = Console.ReadLine();
                Console.WriteLine();

                if(userInput == practiseData.Questions[i].NumOfCorrectOption)
                {
                    correctAnswers++;
                }
            }

            Console.WriteLine(practiseData.Instructions);
            RunInteractive(practiseData.InterectiveArgs);

            if(PractiseIsPassed(practiseData, correctAnswers))
            {
                practiseData.IsPassed = true;
                UpdatePractiseData(practiseData);
            }
        }

        private static void RunInteractive(List<string> interectiveArgs)
        {
            switch(interectiveArgs[0])
            {
                case "BinarySearch":
                {
                    Console.WriteLine(": ");
                    int[] arr = WriteArray();

                    Console.WriteLine(": ");
                    int target = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine(Search.BinarySearch(arr, target));
                    break;
                }

                case "InterpolationSearch":
                {
                    Console.WriteLine(": ");
                    int[] arr = WriteArray();

                    Console.WriteLine(": ");
                    int target = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine(Search.InterpolationSearch(arr, target));
                    break;
                }

                case "ExponentialSearch":
                {
                    Console.WriteLine(": ");
                    int[] arr = WriteArray();

                    Console.WriteLine(": ");
                    int target = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine(Search.ExponentialSearch(arr, target));
                    break;
                }

                case "BubbleSort":
                {
                    Console.WriteLine(": ");
                    int[] arr = WriteArray();

                    arr = Sort.BubbleSort(arr);
                    foreach(int i in arr)
                    {
                        Console.Write($" {i}");
                    }

                    break;
                }

                case "InsertionSort":
                {
                    Console.WriteLine(": ");
                    int[] arr = WriteArray();

                    arr = Sort.InsertionSort(arr);
                    foreach(int i in arr)
                    {
                        Console.Write($" {i}");
                    }
                    
                    break;
                }

                case "CombSort":
                {
                    Console.WriteLine(": ");
                    int[] arr = WriteArray();

                    arr = Sort.CombSort(arr);
                    foreach(int i in arr)
                    {
                        Console.Write($" {i}");
                    }
                    
                    break;
                }

                case "BucketSort":
                {
                    Console.WriteLine(": ");
                    int[] arr = WriteArray();

                    arr = Sort.BucketSort(arr);
                    foreach(int i in arr)
                    {
                        Console.Write($" {i}");
                    }
                    
                    break;
                }

                case "Recursion":
                {
                    Console.WriteLine(": ");
                    int x = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine(Recursion.Factorial(x));
                    
                    break;
                }

                case "MergeSort":
                {
                    Console.WriteLine(": ");
                    int[] arr = WriteArray();

                    arr = Sort.MergeSort(arr);
                    foreach(int i in arr)
                    {
                        Console.Write($" {i}");
                    }

                    break;
                }

                case "QuickSort":
                {
                    Console.WriteLine(": ");
                    int[] arr = WriteArray();

                    Console.WriteLine(": ");
                    int l = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(": ");
                    int r = Convert.ToInt32(Console.ReadLine());

                    arr = Sort.QuickSort(arr, l, r);

                    foreach(int i in arr)
                    {
                        Console.Write($" {i}");
                    }

                    break;
                }
            }
        }

        private static int[] WriteArray()
        {
            string inputArr = Console.ReadLine();

            int[] temp = new int[inputArr.Length];
            int count = 0;

            string number = "";

            for(int i = 0; i < inputArr.Length; i++)
            {
                if(inputArr[i] != ' ')
                {
                    number+=inputArr[i];
                }
                else
                {
                    temp[count] = Convert.ToInt32(number);
                    number = "";
                    count++;
                }
            }

            if(number != "")
            {
                temp[count] = Convert.ToInt32(number);
                count++;
            }

            int[] arr = new int[count];

            for(int i = 0; i < count; i++)
            {
                arr[i] = temp[i];
            }

            return arr;
        }

        private static bool PractiseIsPassed(PractisesPayload practiseData, int correctAnswers)
        {
            if(correctAnswers < 0)
            {
                throw new ArgumentException("Incorrect number of correct answers!");
            }

            if(practiseData.Questions.Count == correctAnswers)
            {
                return true;
            }

            return false;
        }

        private static void UpdatePractiseData(PractisesPayload practiseData)
        {
            List<PractisesPayload> practises = LoadPractisesData();

            for(int i = 0; i < practises.Count; i++)
            {
                if (practises[i].Name == practiseData.Name && practises[i].Number == practiseData.Number)
                {
                    practises[i] = practiseData;
                }
            }
            
            string text = JsonSerializer.Serialize<List<PractisesPayload>>(practises, new JsonSerializerOptions {WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
            File.WriteAllText("Data/PractisesData.json", text);
        }

        private static PractisesPayload GetRequiredPractise(string name, int num)
        {
            List<PractisesPayload> practises = LoadPractisesData();

            for(int i = 0; i < practises.Count; i++)
            {
                if(practises[i].Name == name && practises[i].Number == num)
                {
                    return practises[i];
                }
            }

            throw new ArgumentException("There is no practise with that name or number!");
        }

        private static List<PractisesPayload> LoadPractisesData()
        {
            string text = File.ReadAllText("Data/PractisesData.json");
            List<PractisesPayload> practiseData = JsonSerializer.Deserialize<List<PractisesPayload>>(text, new JsonSerializerOptions {WriteIndented = true});

            return practiseData;
        }
    }
}