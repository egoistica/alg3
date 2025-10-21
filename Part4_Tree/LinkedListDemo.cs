using System.Diagnostics;
using ScottPlot;

// Демонстрация и бенчмарк алгоритмов работы со связными списками
public class LinkedListDemo
{
    public void RunAllDemonstrations()
    {
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ АЛГОРИТМОВ РАБОТЫ СО СВЯЗНЫМИ СПИСКАМИ ===\n");
        
        // Демонстрация всех 12 алгоритмов
        DemonstrateAlgorithm1_Reverse();
        DemonstrateAlgorithm2_MoveElements();
        DemonstrateAlgorithm3_CountDistinct();
        DemonstrateAlgorithm4_RemoveNonUnique();
        DemonstrateAlgorithm5_InsertListAfter();
        DemonstrateAlgorithm6_InsertInSorted();
        DemonstrateAlgorithm7_RemoveAllOccurrences();
        DemonstrateAlgorithm8_InsertBefore();
        DemonstrateAlgorithm9_AppendLists();
        DemonstrateAlgorithm10_SplitList();
        DemonstrateAlgorithm11_DoubleList();
        DemonstrateAlgorithm12_SwapElements();
    }

    private void DemonstrateAlgorithm1_Reverse()
    {
        Console.WriteLine("1. ПЕРЕВОРОТ СПИСКА");
        int[] data = { 1, 2, 3, 4, 5 };
        var head = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head, "Исходный список");
        
        var reversed = LinkedListAlgorithms.ReverseList(head);
        LinkedListAlgorithms.PrintList(reversed, "Перевернутый список");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm2_MoveElements()
    {
        Console.WriteLine("2. ПЕРЕНОС ЭЛЕМЕНТОВ");
        int[] data = { 1, 2, 3, 4, 5 };
        
        // Перенос последнего в начало
        var head1 = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head1, "Исходный список");
        var moved1 = LinkedListAlgorithms.MoveLastToFirst(head1);
        LinkedListAlgorithms.PrintList(moved1, "Последний элемент в начало");
        
        // Перенос первого в конец
        var head2 = LinkedListAlgorithms.CreateListFromArray(data);
        var moved2 = LinkedListAlgorithms.MoveFirstToLast(head2);
        LinkedListAlgorithms.PrintList(moved2, "Первый элемент в конец");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm3_CountDistinct()
    {
        Console.WriteLine("3. ПОДСЧЕТ РАЗЛИЧНЫХ ЭЛЕМЕНТОВ");
        int[] data = { 1, 2, 2, 3, 3, 3, 4, 5, 5 };
        var head = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head, "Список с повторениями");
        
        int distinctCount = LinkedListAlgorithms.CountDistinctElements(head);
        Console.WriteLine($"Количество различных элементов: {distinctCount}");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm4_RemoveNonUnique()
    {
        Console.WriteLine("4. УДАЛЕНИЕ НЕУНИКАЛЬНЫХ ЭЛЕМЕНТОВ");
        int[] data = { 1, 2, 2, 3, 3, 3, 4, 5, 5 };
        var head = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head, "Исходный список");
        
        var unique = LinkedListAlgorithms.RemoveNonUniqueElements(head);
        LinkedListAlgorithms.PrintList(unique, "Только уникальные элементы");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm5_InsertListAfter()
    {
        Console.WriteLine("5. ВСТАВКА СПИСКА ПОСЛЕ ПЕРВОГО ВХОЖДЕНИЯ");
        int[] data = { 1, 2, 3, 4, 5 };
        var head = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head, "Исходный список");
        
        var result = LinkedListAlgorithms.InsertListAfterFirstOccurrence(head, 3);
        LinkedListAlgorithms.PrintList(result, "После вставки списка после элемента 3");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm6_InsertInSorted()
    {
        Console.WriteLine("6. ВСТАВКА В УПОРЯДОЧЕННЫЙ СПИСОК");
        int[] data = { 1, 3, 5, 7, 9 };
        var head = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head, "Упорядоченный список");
        
        var result = LinkedListAlgorithms.InsertInSortedList(head, 6);
        LinkedListAlgorithms.PrintList(result, "После вставки элемента 6");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm7_RemoveAllOccurrences()
    {
        Console.WriteLine("7. УДАЛЕНИЕ ВСЕХ ВХОЖДЕНИЙ ЭЛЕМЕНТА");
        int[] data = { 1, 2, 2, 3, 2, 4, 2, 5 };
        var head = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head, "Исходный список");
        
        var result = LinkedListAlgorithms.RemoveAllOccurrences(head, 2);
        LinkedListAlgorithms.PrintList(result, "После удаления всех вхождений элемента 2");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm8_InsertBefore()
    {
        Console.WriteLine("8. ВСТАВКА ПЕРЕД ПЕРВЫМ ВХОЖДЕНИЕМ");
        int[] data = { 1, 2, 3, 4, 5 };
        var head = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head, "Исходный список");
        
        var result = LinkedListAlgorithms.InsertBeforeFirstOccurrence(head, 99, 3);
        LinkedListAlgorithms.PrintList(result, "После вставки 99 перед элементом 3");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm9_AppendLists()
    {
        Console.WriteLine("9. ДОПИСЫВАНИЕ СПИСКА К СПИСКУ");
        int[] data1 = { 1, 2, 3 };
        int[] data2 = { 4, 5, 6 };
        var list1 = LinkedListAlgorithms.CreateListFromArray(data1);
        var list2 = LinkedListAlgorithms.CreateListFromArray(data2);
        
        LinkedListAlgorithms.PrintList(list1, "Первый список");
        LinkedListAlgorithms.PrintList(list2, "Второй список");
        
        var result = LinkedListAlgorithms.AppendList(list1, list2);
        LinkedListAlgorithms.PrintList(result, "Объединенный список");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm10_SplitList()
    {
        Console.WriteLine("10. РАЗБИЕНИЕ СПИСКА ПО ПЕРВОМУ ВХОЖДЕНИЮ");
        int[] data = { 1, 2, 3, 4, 5, 6, 7 };
        var head = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head, "Исходный список");
        
        var (firstList, secondList) = LinkedListAlgorithms.SplitList(head, 4);
        LinkedListAlgorithms.PrintList(firstList, "Первый список (до элемента 4)");
        LinkedListAlgorithms.PrintList(secondList, "Второй список (после элемента 4)");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm11_DoubleList()
    {
        Console.WriteLine("11. УДВОЕНИЕ СПИСКА");
        int[] data = { 1, 2, 3 };
        var head = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head, "Исходный список");
        
        var doubled = LinkedListAlgorithms.DoubleList(head);
        LinkedListAlgorithms.PrintList(doubled, "Удвоенный список");
        Console.WriteLine();
    }

    private void DemonstrateAlgorithm12_SwapElements()
    {
        Console.WriteLine("12. ПЕРЕСТАНОВКА ДВУХ ЭЛЕМЕНТОВ");
        int[] data = { 1, 2, 3, 4, 5 };
        var head = LinkedListAlgorithms.CreateListFromArray(data);
        LinkedListAlgorithms.PrintList(head, "Исходный список");
        
        var swapped = LinkedListAlgorithms.SwapElements(head, 2, 4);
        LinkedListAlgorithms.PrintList(swapped, "После перестановки элементов 2 и 4");
        Console.WriteLine();
    }

    public void RunPerformanceBenchmarks()
    {
        Console.WriteLine("\n=== БЕНЧМАРК ПРОИЗВОДИТЕЛЬНОСТИ АЛГОРИТМОВ ===\n");
        
        BenchmarkReverseAlgorithm();
        BenchmarkCountDistinctAlgorithm();
        BenchmarkRemoveNonUniqueAlgorithm();
        BenchmarkInsertInSortedAlgorithm();
    }

    private void BenchmarkReverseAlgorithm()
    {
        Console.WriteLine("--- Производительность переворота списка ---");
        
        var sizes = new List<int>();
        var times = new List<double>();
        
        for (int size = 1000; size <= 50000; size += 2000)
        {
            var data = GenerateRandomArray(size);
            var head = LinkedListAlgorithms.CreateListFromArray(data);
            
            var stopwatch = Stopwatch.StartNew();
            LinkedListAlgorithms.ReverseList(head);
            stopwatch.Stop();
            
            sizes.Add(size);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Размер: {size}, Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
        }
        
        CreatePerformancePlot("Reverse Algorithm", sizes, times, "Размер списка", "Время (мс)", "reverse_algorithm_performance.png");
    }

    private void BenchmarkCountDistinctAlgorithm()
    {
        Console.WriteLine("\n--- Производительность подсчета различных элементов ---");
        
        var sizes = new List<int>();
        var times = new List<double>();
        
        for (int size = 1000; size <= 50000; size += 2000)
        {
            var data = GenerateRandomArray(size, 100); // Ограничиваем диапазон для большего количества дубликатов
            var head = LinkedListAlgorithms.CreateListFromArray(data);
            
            var stopwatch = Stopwatch.StartNew();
            LinkedListAlgorithms.CountDistinctElements(head);
            stopwatch.Stop();
            
            sizes.Add(size);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Размер: {size}, Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
        }
        
        CreatePerformancePlot("Count Distinct Algorithm", sizes, times, "Размер списка", "Время (мс)", "count_distinct_performance.png");
    }

    private void BenchmarkRemoveNonUniqueAlgorithm()
    {
        Console.WriteLine("\n--- Производительность удаления неуникальных элементов ---");
        
        var sizes = new List<int>();
        var times = new List<double>();
        
        for (int size = 1000; size <= 30000; size += 1500)
        {
            var data = GenerateRandomArray(size, 50); // Много дубликатов
            var head = LinkedListAlgorithms.CreateListFromArray(data);
            
            var stopwatch = Stopwatch.StartNew();
            LinkedListAlgorithms.RemoveNonUniqueElements(head);
            stopwatch.Stop();
            
            sizes.Add(size);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Размер: {size}, Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
        }
        
        CreatePerformancePlot("Remove Non-Unique Algorithm", sizes, times, "Размер списка", "Время (мс)", "remove_non_unique_performance.png");
    }

    private void BenchmarkInsertInSortedAlgorithm()
    {
        Console.WriteLine("\n--- Производительность вставки в упорядоченный список ---");
        
        var sizes = new List<int>();
        var times = new List<double>();
        
        for (int size = 1000; size <= 30000; size += 1500)
        {
            var data = GenerateSortedArray(size);
            var head = LinkedListAlgorithms.CreateListFromArray(data);
            
            var stopwatch = Stopwatch.StartNew();
            LinkedListAlgorithms.InsertInSortedList(head, size / 2);
            stopwatch.Stop();
            
            sizes.Add(size);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Размер: {size}, Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
        }
        
        CreatePerformancePlot("Insert In Sorted Algorithm", sizes, times, "Размер списка", "Время (мс)", "insert_sorted_performance.png");
    }

    private int[] GenerateRandomArray(int size, int maxValue = 1000)
    {
        var random = new Random();
        var array = new int[size];
        
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(maxValue);
        }
        
        return array;
    }

    private int[] GenerateSortedArray(int size)
    {
        var array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = i;
        }
        return array;
    }

    private void CreatePerformancePlot(string title, List<int> sizes, List<double> times, 
        string xLabel, string yLabel, string filename)
    {
        var plot = new Plot();
        
        double[] xValues = sizes.Select(s => (double)s).ToArray();
        double[] yValues = times.ToArray();
        
        plot.Add.Scatter(xValues, yValues);
        plot.XLabel(xLabel);
        plot.YLabel(yLabel);
        plot.Title($"{title} - Производительность");
        
        plot.SavePng(filename, 800, 600);
        Console.WriteLine($"График сохранен в {filename}");
    }
}
