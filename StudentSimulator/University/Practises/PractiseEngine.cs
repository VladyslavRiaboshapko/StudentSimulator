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
                
                case "BinarySearch":        ExecuteSearchJob(interectiveArgs, Search.BinarySearch); break;
                case "InterpolationSearch": ExecuteSearchJob(interectiveArgs, Search.InterpolationSearch); break;
                case "ExponentialSearch":   ExecuteSearchJob(interectiveArgs, Search.ExponentialSearch); break;

                
                case "BubbleSort":    ExecuteSortJob(Sort.BubbleSort); break;
                case "InsertionSort": ExecuteSortJob(Sort.InsertionSort); break;
                case "CombSort":      ExecuteSortJob(Sort.CombSort); break;
                case "BucketSort":    ExecuteSortJob(Sort.BucketSort); break;
                case "MergeSort":     ExecuteSortJob(Sort.MergeSort); break;

                
                case "Recursion":          ExecuteRecursion(); break;
                case "QuickSort":          ExecuteQuickSort(); break;
                case "ChemicalConstructor":ExecuteChemicalConstructor(interectiveArgs); break;
                case "PhysicsEngine":      ExecutePhysicsEngine(interectiveArgs); break;
                case "SentenceBuilder":    ExecuteSentenceBuilder(interectiveArgs); break;
                case "EquationSolver":     ExecuteEquationSolver(interectiveArgs); break;
                case "CodeFixer":          ExecuteCodeFixer(interectiveArgs); break;
            }
        }

        private static void ExecuteRecursion()
        {
            Console.WriteLine(": ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(Recursion.Factorial(x));
        }

        private static void ExecuteQuickSort()
        {
            Console.WriteLine(": ");
            int[] arr = WriteArray();

            Console.WriteLine(": ");
            int l = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(": ");
            int r = Convert.ToInt32(Console.ReadLine());

            arr = Sort.QuickSort(arr, l, r);
            foreach(int i in arr) Console.Write($" {i}");
            Console.WriteLine();
        }

        private static void ExecuteChemicalConstructor(List<string> args)
        {
            for(int i = 1; i < args.Count - 1; i++)
            {
                Console.Write($"{args[i]} ");
            }
            List<int> correctIndices = ParseCorrectIndices(args[args.Count - 1]);
            Console.WriteLine("\nВведіть правильні відповіді: ");
            
            int count = 0;
            int[] userInput = WriteArray();
            for(int i = 0; i < correctIndices.Count; i++)
            {
                if (i < userInput.Length && correctIndices[i] == userInput[i]) count++;
            }
            
            if(count == correctIndices.Count) Console.WriteLine("Молодець!");
            else
            {
                Console.WriteLine("Неправильно!");
                foreach(int i in correctIndices) Console.Write($"{i} ");
            }
            Console.WriteLine();
        }

        private static void ExecutePhysicsEngine(List<string> args)
        {
            string physicsVariable = args[1]; 
            string correctValueStr = args[args.Count - 1];

            Console.WriteLine($"\n--- Фізичний симулятор ---");
            Console.WriteLine($"Завдання: Встановіть правильне значення для: {physicsVariable}");
            Console.Write("Ваша відповідь: ");

            string? rawInput = Console.ReadLine();
            if (rawInput == null) return;

            if (IsPhysicsAnswerCorrect(rawInput, correctValueStr))
                Console.WriteLine("✅ Правильно! Калібрування системи завершено успішно.");
            else
                Console.WriteLine($"❌ Помилка! Система вийшла з ладу.\nПравильне значення: {correctValueStr}");
        }

        private static void ExecuteSentenceBuilder(List<string> args)
        {
            for (int i = 1; i < args.Count - 1; i++) Console.Write($"[{i}] {args[i]}  ");
            Console.WriteLine("\nСкладіть речення, ввівши номери слів через пробіл:");

            List<int> correctIndices = ParseCorrectIndices(args[args.Count - 1]);
            int[] userInput = WriteArray();

            int correctCount = 0;
            if (userInput.Length == correctIndices.Count)
            {
                for (int i = 0; i < correctIndices.Count; i++)
                {
                    if (userInput[i] == correctIndices[i]) correctCount++;
                }
            }

            if (correctCount == correctIndices.Count) Console.WriteLine("Чудово! Ваша граматика бездоганна.");
            else Console.WriteLine("Неправильно. Спробуйте переглянути порядок слів.");
        }

        private static void ExecuteEquationSolver(List<string> args)
        {
            string formula = args[1]; 
            string targetAnswer = args[args.Count - 1];

            Console.WriteLine($"\n--- Математичний практикум ---");
            Console.WriteLine($"Завдання: {formula}");
            Console.Write("Ваша відповідь: ");

            string? userInput = Console.ReadLine();
            if (userInput == null) return;

            if (IsPhysicsAnswerCorrect(userInput, targetAnswer)) Console.WriteLine("✅ Правильно! Обчислення вірні.");
            else Console.WriteLine($"❌ Помилка в розрахунках.\nПравильна відповідь: {targetAnswer}");
        }

        private static void ExecuteCodeFixer(List<string> args)
        {
            string snippet = args[1]; 
            string targetToken = args[args.Count - 1];

            Console.WriteLine($"\n--- Лабораторні заняття з програмування: Аналіз коду ---");
            Console.WriteLine($"Code/Task: {snippet}");
            Console.Write("Ваша відповідь (ключове слово/значення): ");

            string? userInput = Console.ReadLine();
            if (userInput == null) return;

            if (IsPhysicsAnswerCorrect(userInput.ToLower(), targetToken.ToLower())) Console.WriteLine("✅ Правильно! Логіка перевірена.");
            else Console.WriteLine($"❌ Помилка компіляції. Очікувалося: {targetToken}");
        }

        private static void ExecuteSearchJob(List<string> args, Func<int[], int, int> searchAlgorithm)
        {
            Console.WriteLine(": ");
            int[] arr = WriteArray();

            Console.WriteLine(": ");
            int target = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine(searchAlgorithm(arr, target));
        }

        private static void ExecuteSortJob(Func<int[], int[]> sortAlgorithm)
        {
            Console.WriteLine(": ");
            int[] arr = WriteArray();

            arr = sortAlgorithm(arr);
            foreach(int i in arr)
            {
                Console.Write($" {i}");
            }
            Console.WriteLine();
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
                throw new ArgumentException("Некоректне число правильних відповідей!");
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

            throw new ArgumentException("Практик із такою назвою чи номером не існує!");
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

            while (i < input.Length && input[i] == ' ')
            {
                i++;
            }
            while (j < target.Length && target[j] == ' ')
            {
                j++;
            }

            while (i < input.Length && j < target.Length)
            {
                char charInput = input[i];
                char charTarget = target[j];

                if (charInput == ',')
                {
                    charInput = '.';
                }
                if (charTarget == ',')
                {
                    charTarget = '.';
                }

                if (charInput != charTarget)
                {
                    return false;
                }

                i++;
                j++;
            }

            while (i < input.Length && input[i] == ' ')
            {
                i++;
            }
            while (j < target.Length && target[j] == ' ')
            {
                j++;
            }

            return i == input.Length && j == target.Length;
        }
    }
}