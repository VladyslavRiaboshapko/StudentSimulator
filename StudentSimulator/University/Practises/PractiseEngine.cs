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
            RunInteractive(practiseData.InteractiveArgs);

            if(PractiseIsPassed(practiseData, correctAnswers))
            {
                practiseData.IsPassed = true;
                UpdatePractiseData(practiseData);
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися...");
            Console.ReadKey();
        }

        private static void RunInteractive(List<string> interectiveArgs)
        {
            if (interectiveArgs == null || interectiveArgs.Count == 0)
            {
                Console.WriteLine("\nІнтерактивна частина для цієї практики відсутня або не налаштована!");
                Console.WriteLine("Натисніть будь-яку клавішу для завершення практики...");
                Console.ReadKey();
                return; 
            }

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
                case "ChemicalConstructor" :
                {
                    for(int i = 1; i < interectiveArgs.Count - 1; i++)
                    {
                        Console.Write($"{interectiveArgs[i]} ");
                    }
                    List<int> correctIndices = ParseCorrectIndices(interectiveArgs[interectiveArgs.Count - 1]);
                    Console.WriteLine();
                    Console.WriteLine("Введіть правильні відповіді: ");
                    int count = 0;
                    int[] userInput = WriteArray();
                    for(int i = 0; i < correctIndices.Count; i++)
                    {
                        if (i < userInput.Length && correctIndices[i] == userInput[i])
                        {
                            count++;
                        }
                    }
                    if(count == correctIndices.Count)
                    {
                        Console.WriteLine("Молодець!");
                    }
                    else
                    {
                        Console.WriteLine("Неправильно!");
                        foreach(int i in correctIndices)
                        {
                            Console.Write($"{i} ");
                        }
                    }
                    break;
                }
                case "PhysicsEngine":
                {
                    string physicsVariable = interectiveArgs[1]; 
                    string correctValueStr = interectiveArgs[interectiveArgs.Count - 1];

                    Console.WriteLine($"\n--- Фізичний симулятор ---");
                    Console.WriteLine($"Завдання: Встановіть правильне значення для: {physicsVariable}");
                    Console.Write("Ваша відповідь: ");

                    string? rawInput = Console.ReadLine();
                    if (rawInput == null) break;

    
                    if (IsPhysicsAnswerCorrect(rawInput, correctValueStr))
                    {
                        Console.WriteLine("✅ Правильно! Калібрування системи завершено успішно.");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Помилка! Система вийшла з ладу.");
                        Console.WriteLine($"Правильне значення мало бути: {correctValueStr}");
                    }
                    break;
                }
                case "SentenceBuilder":
                {
                    for (int i = 1; i < interectiveArgs.Count - 1; i++)
                    {
                        Console.Write($"[{i}] {interectiveArgs[i]}  ");
                    }
                    Console.WriteLine("\nСкладіть речення, ввівши номери слів через пробіл:");

                    List<int> correctIndices = ParseCorrectIndices(interectiveArgs[interectiveArgs.Count - 1]);
                    int[] userInput = WriteArray();

                    int correctCount = 0;
                    if (userInput.Length == correctIndices.Count)
                    {
                        for (int i = 0; i < correctIndices.Count; i++)
                        {
                            if (userInput[i] == correctIndices[i]) correctCount++;
                        }
                    }

                    if (correctCount == correctIndices.Count)
                    {
                        Console.WriteLine("Perfect! Your grammar is correct.");
                    }
                    else
                    {
                        Console.WriteLine("Incorrect. Try to review the word order.");
                    }
                    break;
                }
                case "EquationSolver":
                {
                    string formula = interectiveArgs[1]; 
                    string targetAnswer = interectiveArgs[interectiveArgs.Count - 1];

                    Console.WriteLine($"\n--- Математичний практикум ---");
                    Console.WriteLine($"Завдання: {formula}");
                    Console.Write("Ваша відповідь: ");

                    string? userInput = Console.ReadLine();
                    if (userInput == null) break;

                    if (IsPhysicsAnswerCorrect(userInput, targetAnswer))
                    {
                        Console.WriteLine("✅ Правильно! Обчислення вірні.");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Помилка в розрахунках.");
                        Console.WriteLine($"Правильна відповідь: {targetAnswer}");
                    }
                    break;
                }
                case "CodeFixer":
                {
                    string snippet = interectiveArgs[1]; 
                    string targetToken = interectiveArgs[interectiveArgs.Count - 1];

                    Console.WriteLine($"\n--- Programming Lab: Code Analysis ---");
                    Console.WriteLine($"Code/Task: {snippet}");
                    Console.Write("Your answer (keyword/value): ");

                    string? userInput = Console.ReadLine();
                    if (userInput == null) break;

                    if (IsPhysicsAnswerCorrect(userInput.ToLower(), targetToken.ToLower()))
                    {
                        Console.WriteLine("✅ Correct! Logic verified.");
                    }
                    else
                    {
                        Console.WriteLine($"❌ Compilation Error. Expected: {targetToken}");
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

        public static List<PractisesPayload> LoadPractisesData()
        {
            string text = File.ReadAllText("Data/PractisesData.json");
            List<PractisesPayload> practiseData = JsonSerializer.Deserialize<List<PractisesPayload>>(text, new JsonSerializerOptions {WriteIndented = true});

            return practiseData;
        }

        private static List<int> ParseCorrectIndices(string correctTag)
        {
            List<int> indices = new List<int>();

            for (int i = 8; i < correctTag.Length; i++)
            {
                char c = correctTag[i];

                if (c >= '0' && c <= '9')
                {
                    int value = c - '0'; 
                    indices.Add(value - 1);
                }
            }

            return indices;
        }

        private static bool IsPhysicsAnswerCorrect(string input, string target)
        {
            int i = 0;
            int j = 0;

            while (i < input.Length && input[i] == ' ') i++;
            while (j < target.Length && target[j] == ' ') j++;

            while (i < input.Length && j < target.Length)
            {
                char charInput = input[i];
                char charTarget = target[j];

                if (charInput == ',') charInput = '.';
                if (charTarget == ',') charTarget = '.';

                if (charInput != charTarget) return false;

                i++;
                j++;
            }

            while (i < input.Length && input[i] == ' ') i++;
            while (j < target.Length && target[j] == ' ') j++;

            return i == input.Length && j == target.Length;
        }
    }
}