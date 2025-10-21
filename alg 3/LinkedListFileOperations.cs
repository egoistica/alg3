// Функции для работы с файлами в алгоритмах 9-10
public class LinkedListFileOperations
{
    // 9. Функция дописывания списка E к списку L (чтение из файлов)
    public static Node<int> AppendListsFromFiles(string fileL, string fileE)
    {
        var listL = ReadListFromFile(fileL);
        var listE = ReadListFromFile(fileE);
        
        Console.WriteLine("Чтение списков из файлов:");
        LinkedListAlgorithms.PrintList(listL, $"Список L из файла {fileL}");
        LinkedListAlgorithms.PrintList(listE, $"Список E из файла {fileE}");
        
        var result = LinkedListAlgorithms.AppendList(listL, listE);
        LinkedListAlgorithms.PrintList(result, "Объединенный список L + E");
        
        return result;
    }

    // 10. Функция разбиения списка на два по первому вхождению (чтение из файла)
    public static (Node<int> firstList, Node<int> secondList) SplitListFromFile(string filename, int splitValue)
    {
        var head = ReadListFromFile(filename);
        
        Console.WriteLine($"Чтение списка из файла {filename}:");
        LinkedListAlgorithms.PrintList(head, "Исходный список");
        
        var (firstList, secondList) = LinkedListAlgorithms.SplitList(head, splitValue);
        
        LinkedListAlgorithms.PrintList(firstList, $"Первый список (до элемента {splitValue})");
        LinkedListAlgorithms.PrintList(secondList, $"Второй список (после элемента {splitValue})");
        
        return (firstList, secondList);
    }

    // Чтение списка целых чисел из файла
    public static Node<int> ReadListFromFile(string filename)
    {
        try
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"Файл {filename} не найден. Создаю тестовый файл...");
                CreateTestFile(filename);
            }

            string content = File.ReadAllText(filename);
            string[] numbers = content.Split(new[] { ' ', '\t', '\n', '\r' }, 
                                           StringSplitOptions.RemoveEmptyEntries);
            
            var numbersList = new List<int>();
            foreach (string numStr in numbers)
            {
                if (int.TryParse(numStr, out int num))
                {
                    numbersList.Add(num);
                }
            }
            
            return LinkedListAlgorithms.CreateListFromArray(numbersList.ToArray());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла {filename}: {ex.Message}");
            return null;
        }
    }

    // Запись списка в файл
    public static void WriteListToFile(Node<int> head, string filename)
    {
        try
        {
            var numbers = LinkedListAlgorithms.ListToArray(head);
            string content = string.Join(" ", numbers);
            File.WriteAllText(filename, content);
            Console.WriteLine($"Список записан в файл {filename}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи в файл {filename}: {ex.Message}");
        }
    }

    // Создание тестового файла с числами
    private static void CreateTestFile(string filename)
    {
        var random = new Random();
        var numbers = new List<int>();
        
        // Генерируем 10-20 случайных чисел
        int count = random.Next(10, 21);
        for (int i = 0; i < count; i++)
        {
            numbers.Add(random.Next(1, 100));
        }
        
        string content = string.Join(" ", numbers);
        File.WriteAllText(filename, content);
        Console.WriteLine($"Создан тестовый файл {filename} с {count} числами");
    }

    // Демонстрация работы с файлами
    public static void DemonstrateFileOperations()
    {
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ РАБОТЫ С ФАЙЛАМИ ===\n");
        
        // Создаем тестовые файлы
        CreateTestFile("listL.txt");
        CreateTestFile("listE.txt");
        CreateTestFile("splitList.txt");
        
        Console.WriteLine("\n--- Алгоритм 9: Дописывание списков из файлов ---");
        var appendedList = AppendListsFromFiles("listL.txt", "listE.txt");
        
        Console.WriteLine("\n--- Алгоритм 10: Разбиение списка из файла ---");
        var (firstList, secondList) = SplitListFromFile("splitList.txt", 50);
        
        // Сохраняем результаты
        WriteListToFile(appendedList, "appended_result.txt");
        WriteListToFile(firstList, "first_part.txt");
        WriteListToFile(secondList, "second_part.txt");
    }

    // Создание файлов для демонстрации
    public static void CreateDemoFiles()
    {
        Console.WriteLine("Создание демонстрационных файлов...");
        
        // Файл для списка L
        File.WriteAllText("listL.txt", "1 2 3 4 5");
        
        // Файл для списка E  
        File.WriteAllText("listE.txt", "6 7 8 9 10");
        
        // Файл для разбиения
        File.WriteAllText("splitList.txt", "10 20 30 40 50 60 70 80 90");
        
        Console.WriteLine("Демонстрационные файлы созданы:");
        Console.WriteLine("- listL.txt: 1 2 3 4 5");
        Console.WriteLine("- listE.txt: 6 7 8 9 10");
        Console.WriteLine("- splitList.txt: 10 20 30 40 50 60 70 80 90");
    }
}
