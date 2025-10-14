using System.Diagnostics;
using ScottPlot;

public class Benchmark
{
    private FileGenerator _fileGenerator = new FileGenerator();
    private StackOperation _operationExecutor = new StackOperation();
    
    public void Run()
    {
        var sizes = new List<int>();
        var times = new List<double>();
        
        // Тестируем от 1 до 1000 операций с шагом 50 (для наглядности)
        for (int size = 1; size <= 5000; size += 200)
        {
            // Генерируем файл с заданным количеством операций
            _fileGenerator.GenerateFile(size);
            
            // Замеряем время выполнения
            var stopwatch = Stopwatch.StartNew();
            _operationExecutor.MakeOperation();
            stopwatch.Stop();
            
            sizes.Add(size);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Операций: {size}, Время: {stopwatch.Elapsed.TotalMilliseconds} мс");
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
        plot.XLabel("Количество операций");
        plot.YLabel("Время выполнения (мс)");
        plot.Title("Зависимость времени выполнения от количества операций");
        
        // Сохраняем график в файл
        plot.SavePng("Первый.png", 800, 600);
        Console.WriteLine("График сохранен в Первый.png");
    }
}