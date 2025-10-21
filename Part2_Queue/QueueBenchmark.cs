using System.Diagnostics;
using ScottPlot;

public class QueueBenchmark
{
    private QueueFileGenerator _fileGenerator = new QueueFileGenerator();
    private QueueOperation _operationExecutor = new QueueOperation();
    
    public void Run()
    {
        Console.WriteLine("=== Бенчмарк очередей ===");
        
        // Тест 1: Различные по длине наборы операций
        TestDifferentLengths();
        
        // Тест 2: Одинаковые по длине, но различные по составу операций
        TestDifferentCompositions();
    }
    
    private void TestDifferentLengths()
    {
        Console.WriteLine("\n--- Тест 1: Различные по длине наборы операций ---");
        
        var sizes = new List<int>();
        var customTimes = new List<double>();
        var standardTimes = new List<double>();
        
        // Тестируем от 100 до 10000 операций с шагом 500
        for (int size = 100; size <= 5000; size += 500)
        {
            Console.WriteLine($"Тестируем {size} операций...");
            
            // Генерируем файл с заданным количеством операций
            _fileGenerator.GenerateFile(size);
            
            // Тест собственной реализации
            var stopwatch1 = Stopwatch.StartNew();
            _operationExecutor.MakeOperationWithCustomQueue();
            stopwatch1.Stop();
            
            // Тест стандартной реализации
            var stopwatch2 = Stopwatch.StartNew();
            _operationExecutor.MakeOperationWithStandardQueue();
            stopwatch2.Stop();
            
            sizes.Add(size);
            customTimes.Add(stopwatch1.Elapsed.TotalMilliseconds);
            standardTimes.Add(stopwatch2.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"  Собственная очередь: {stopwatch1.Elapsed.TotalMilliseconds:F2} мс");
            Console.WriteLine($"  Стандартная очередь: {stopwatch2.Elapsed.TotalMilliseconds:F2} мс");
        }
        
        // Строим график для разных длин
        CreateLengthComparisonPlot(sizes, customTimes, standardTimes);
    }
    
    private void TestDifferentCompositions()
    {
        Console.WriteLine("\n--- Тест 2: Одинаковые по длине, но различные по составу операций ---");
        
        var compositions = new List<string>();
        var customTimes = new List<double>();
        var standardTimes = new List<double>();
        
        int testSize = 2000; // Фиксированный размер для сравнения составов
        
        // Различные составы операций для максимального расхождения
        var testCases = new[]
        {
            ("Только добавления", new[] { 1, 1, 1, 1, 1 }), // 100% добавления
            ("Только удаления", new[] { 2, 2, 2, 2, 2 }), // 100% удаления (после предварительного заполнения)
            ("Только просмотры", new[] { 3, 3, 3, 3, 3 }), // 100% просмотры
            ("Только проверки пустоты", new[] { 4, 4, 4, 4, 4 }), // 100% проверки
            ("Только печать", new[] { 5, 5, 5, 5, 5 }), // 100% печать
            ("Смешанные операции", new[] { 1, 2, 3, 4, 5 }), // Сбалансированный набор
            ("Много добавлений", new[] { 1, 1, 1, 2, 3 }), // 60% добавления
            ("Много удалений", new[] { 1, 2, 2, 2, 3 }), // 60% удаления
        };
        
        foreach (var (name, operations) in testCases)
        {
            Console.WriteLine($"Тестируем состав: {name}");
            
            // Генерируем файл с заданным составом операций
            _fileGenerator.GenerateFileWithComposition(testSize, operations);
            
            // Тест собственной реализации
            var stopwatch1 = Stopwatch.StartNew();
            _operationExecutor.MakeOperationWithCustomQueue();
            stopwatch1.Stop();
            
            // Тест стандартной реализации
            var stopwatch2 = Stopwatch.StartNew();
            _operationExecutor.MakeOperationWithStandardQueue();
            stopwatch2.Stop();
            
            compositions.Add(name);
            customTimes.Add(stopwatch1.Elapsed.TotalMilliseconds);
            standardTimes.Add(stopwatch2.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"  Собственная очередь: {stopwatch1.Elapsed.TotalMilliseconds:F2} мс");
            Console.WriteLine($"  Стандартная очередь: {stopwatch2.Elapsed.TotalMilliseconds:F2} мс");
        }
        
        // Строим график для разных составов
        CreateCompositionComparisonPlot(compositions, customTimes, standardTimes);
    }
    
    private void CreateLengthComparisonPlot(List<int> sizes, List<double> customTimes, List<double> standardTimes)
    {
        var plot = new Plot();
        
        double[] xValues = sizes.Select(s => (double)s).ToArray();
        double[] customYValues = customTimes.ToArray();
        double[] standardYValues = standardTimes.ToArray();
        
        plot.Add.Scatter(xValues, customYValues, label: "Собственная очередь");
        plot.Add.Scatter(xValues, standardYValues, label: "Стандартная очередь");
        
        plot.XLabel("Количество операций");
        plot.YLabel("Время выполнения (мс)");
        plot.Title("Сравнение производительности очередей по длине операций");
        plot.Legend();
        
        plot.SavePng("queue_length_comparison.png", 800, 600);
        Console.WriteLine("График сохранен в queue_length_comparison.png");
    }
    
    private void CreateCompositionComparisonPlot(List<string> compositions, List<double> customTimes, List<double> standardTimes)
    {
        var plot = new Plot();
        
        double[] xValues = Enumerable.Range(0, compositions.Count).Select(i => (double)i).ToArray();
        double[] customYValues = customTimes.ToArray();
        double[] standardYValues = standardTimes.ToArray();
        
        plot.Add.Scatter(xValues, customYValues, label: "Собственная очередь");
        plot.Add.Scatter(xValues, standardYValues, label: "Стандартная очередь");
        
        plot.XLabel("Тип состава операций");
        plot.YLabel("Время выполнения (мс)");
        plot.Title("Сравнение производительности очередей по составу операций");
        plot.Legend();
        
        // Устанавливаем подписи по оси X
        plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
        
        plot.SavePng("queue_composition_comparison.png", 1000, 600);
        Console.WriteLine("График сохранен в queue_composition_comparison.png");
    }
}
