
internal class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Практическое занятие №3: Динамические структуры ===");
        Console.WriteLine();
        
        // Часть 1: Стек
        Console.WriteLine("=== ЧАСТЬ 1: СТЕК ===");
        RunPart1_Stack();
        
        Console.WriteLine("\n" + new string('=', 60) + "\n");
        
        // Часть 2: Очередь
        Console.WriteLine("=== ЧАСТЬ 2: ОЧЕРЕДЬ ===");
        RunPart2_Queue();
        
        Console.WriteLine("\n" + new string('=', 60) + "\n");
        
        // Часть 3: Примеры применения динамических структур
        Console.WriteLine("=== ЧАСТЬ 3: ПРИМЕНЕНИЕ ДИНАМИЧЕСКИХ СТРУКТУР ===");
        RunPart3_Applications();
        
        Console.WriteLine("\n" + new string('=', 60) + "\n");
        
        // Часть 4: Алгоритмы работы со связными списками
        Console.WriteLine("=== ЧАСТЬ 4: АЛГОРИТМЫ СВЯЗНЫХ СПИСКОВ ===");
        RunPart4_LinkedListAlgorithms();
        
        Console.WriteLine("\n" + new string('=', 60) + "\n");
        
        // Демо: конвертация инфикс -> постфикс и вычисление, если есть файл infix.txt
        const string infixPath = "infix.txt";
        if (File.Exists(infixPath))
        {
            Console.WriteLine("=== ДЕМО: ИНФИКС -> ПОСТФИКС ===");
            var converter = new InfixToPostfix();
            string infix = File.ReadAllText(infixPath);
            string postfix = converter.Convert(infix);
            Console.WriteLine($"Инфикс: {infix}");
            Console.WriteLine($"Постфикс: {postfix}");

            // Сохраняем в postfix.txt и считаем
            File.WriteAllText("postfix.txt", postfix);
            var calc = new PostfixCalculate();
            double result = calc.Calculate("postfix.txt");
            Console.WriteLine($"Результат: {result}");
        }
    }
    
    private static void RunPart1_Stack()
    {
        try
        {
            var fb = new Benchmark();
            var sb = new BenchmarkPostfix();
            
            Console.WriteLine("Запуск бенчмарка стека...");
            fb.Run();
            
            Console.WriteLine("Запуск бенчмарка постфиксных вычислений...");
            sb.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в части 1: {ex.Message}");
        }
    }
    
    private static void RunPart2_Queue()
    {
        try
        {
            var qb = new QueueBenchmark();
            
            Console.WriteLine("Запуск бенчмарка очередей...");
            qb.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в части 2: {ex.Message}");
        }
    }
    
    private static void RunPart3_Applications()
    {
        try
        {
            var demo = new DataStructuresDemo();
            
            Console.WriteLine("Запуск демонстрации применений динамических структур...");
            demo.RunAllDemonstrations();
            
            Console.WriteLine("\nЗапуск бенчмарков производительности...");
            demo.RunPerformanceBenchmarks();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в части 3: {ex.Message}");
        }
    }
    
    private static void RunPart4_LinkedListAlgorithms()
    {
        try
        {
            var demo = new LinkedListDemo();
            
            Console.WriteLine("Запуск демонстрации алгоритмов связных списков...");
            demo.RunAllDemonstrations();
            
            Console.WriteLine("\nЗапуск бенчмарков производительности...");
            demo.RunPerformanceBenchmarks();
            
            Console.WriteLine("\nЗапуск демонстрации работы с файлами...");
            LinkedListFileOperations.CreateDemoFiles();
            LinkedListFileOperations.DemonstrateFileOperations();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в части 4: {ex.Message}");
        }
    }
}
