using System.Diagnostics;
using ScottPlot;

public class BenchmarkPostfix
{
    private PostfixFileGenerator _fileGenerator = new PostfixFileGenerator();
    private PostfixCalculate _calculator = new PostfixCalculate();
    
    public void Run()
    {
        WarmUpJIT();
        
        var sizes = new List<int>();
        var times = new List<double>();
        
        // Тестируем от 1000 до 50000 символов с шагом 2000
        for (int symbolCount = 1000; symbolCount <= 5001; symbolCount += 2000)
        {
            // Генерируем файл с заданным количеством символов
            string filename = $"benchmark_{symbolCount}.txt";
            _fileGenerator.GeneratePostfixBySymbolCount(symbolCount, filename);
            
            // Замеряем время выполнения
            var stopwatch = Stopwatch.StartNew();
            double result = _calculator.Calculate();
            stopwatch.Stop();
            
            sizes.Add(symbolCount);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Символов: {symbolCount}, Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс, Результат: {result:F4}");
            
            // Удаляем временный файл
            File.Delete(filename);
        }
        
        // Строим график
        CreateAndSavePlot(sizes, times);
    }
    
    private void CreateAndSavePlot(List<int> sizes, List<double> times)
    {
        var plot = new Plot();
        
        // Преобразуем данные для ScottPlot
        double[] xValues = sizes.Select(s => (double)s).ToArray();
        double[] yValues = times.ToArray();
        
        plot.Add.Scatter(xValues, yValues);
        plot.XLabel("Количество символов в выражении");
        plot.YLabel("Время выполнения (мс)");
        plot.Title("Зависимость времени вычисления постфиксного выражения от размера");
        
        // Сохраняем график в файл
        plot.SavePng("postfix_benchmark.png", 800, 600);
        Console.WriteLine("График сохранен в postfix_benchmark.png");
    }
    
    // Дополнительный метод для тестирования с разными шагами
    public void RunDetailed(int minSymbols = 500, int maxSymbols = 20000, int step = 500)
    {
        var sizes = new List<int>();
        var times = new List<double>();
        
        Console.WriteLine($"Детальный тест: от {minSymbols} до {maxSymbols} символов с шагом {step}");
        
        for (int symbolCount = minSymbols; symbolCount <= maxSymbols; symbolCount += step)
        {
            string filename = $"detailed_{symbolCount}.txt";
            _fileGenerator.GeneratePostfixBySymbolCount(symbolCount, filename);
            
            var stopwatch = Stopwatch.StartNew();
            double result = _calculator.Calculate();
            stopwatch.Stop();
            
            sizes.Add(symbolCount);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Символов: {symbolCount}, Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
            
            File.Delete(filename);
        }
        
        CreateAndSaveDetailedPlot(sizes, times);
    }
    
    private void CreateAndSaveDetailedPlot(List<int> sizes, List<double> times)
    {
        var plot = new Plot();
        
        double[] xValues = sizes.Select(s => (double)s).ToArray();
        double[] yValues = times.ToArray();
        
        plot.Add.Scatter(xValues, yValues);
        plot.XLabel("Количество символов");
        plot.YLabel("Время выполнения (мс)");
        plot.Title("Детальный анализ производительности постфиксного калькулятора");
        
        plot.SavePng("postfix_detailed.png", 800, 600);
        Console.WriteLine("График сохранен в postfix_detailed.png");
    }
    
    private void WarmUpJIT()
    {
        Console.WriteLine("Прогрев JIT компилятора...");
        
        // Создаем маленькое тестовое выражение для прогрева
        _fileGenerator.GeneratePostfixBySymbolCount(100, "warmup.txt");
        
        // Прогреваем основные методы несколько раз
        for (int i = 0; i < 10; i++)
        {
            // Прогреваем калькулятор
            double result = _calculator.Calculate();
            
            // Прогреваем генератор
            _fileGenerator.GeneratePostfixBySymbolCount(50, $"warmup_temp_{i}.txt");
            File.Delete($"warmup_temp_{i}.txt");
        }
        
        // Очистка временных файлов
        if (File.Exists("warmup.txt"))
            File.Delete("warmup.txt");
            
        Console.WriteLine("JIT прогрев завершен");
    }
}