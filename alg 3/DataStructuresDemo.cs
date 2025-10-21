using System.Diagnostics;
using ScottPlot;

// Демонстрация и бенчмарк примеров применения динамических структур данных
public class DataStructuresDemo
{
    public void RunAllDemonstrations()
    {
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ ПРИМЕНЕНИЯ ДИНАМИЧЕСКИХ СТРУКТУР ДАННЫХ ===\n");
        
        // 1. Список - LRU Cache
        Console.WriteLine("1. СПИСОК - LRU Cache (Least Recently Used)");
        Console.WriteLine("Применение: кэширование в веб-серверах, базах данных, ОС");
        DemonstrateLRUCache();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // 2. Стек - Парсер скобок
        Console.WriteLine("2. СТЕК - Парсер скобок с проверкой баланса");
        Console.WriteLine("Применение: компиляторы, IDE, валидация JSON/XML");
        BracketParser.Demonstrate();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // 3. Очередь - BFS обход графа
        Console.WriteLine("3. ОЧЕРЕДЬ - BFS обход графа");
        Console.WriteLine("Применение: поиск кратчайшего пути, анализ социальных сетей");
        GraphBFS.Demonstrate();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // 4. Дерево - Автокомплит
        Console.WriteLine("4. ДЕРЕВО - Автокомплит с префиксным деревом (Trie)");
        Console.WriteLine("Применение: автодополнение в поисковых системах, IDE");
        TrieAutocomplete.Demonstrate();
    }

    private void DemonstrateLRUCache()
    {
        var cache = new LRUCache<string, string>(3);
        
        Console.WriteLine("Демонстрация LRU Cache (размер = 3):");
        
        // Добавляем элементы
        cache.Put("user1", "Иван Петров");
        cache.Put("user2", "Мария Сидорова");
        cache.Put("user3", "Алексей Козлов");
        cache.PrintCache();
        
        Console.WriteLine("\nДоступ к user1 (делаем его недавно использованным):");
        var user1 = cache.Get("user1");
        Console.WriteLine($"Получен: {user1}");
        cache.PrintCache();
        
        Console.WriteLine("\nДобавляем новый элемент (user4), user2 должен быть удален:");
        cache.Put("user4", "Елена Морозова");
        cache.PrintCache();
    }

    public void RunPerformanceBenchmarks()
    {
        Console.WriteLine("\n=== БЕНЧМАРК ПРОИЗВОДИТЕЛЬНОСТИ ===\n");
        
        BenchmarkLRUCache();
        BenchmarkBracketParser();
        BenchmarkGraphBFS();
        BenchmarkTrieAutocomplete();
    }

    private void BenchmarkLRUCache()
    {
        Console.WriteLine("--- LRU Cache Performance ---");
        
        var sizes = new List<int>();
        var times = new List<double>();
        
        for (int size = 100; size <= 10000; size += 500)
        {
            var cache = new LRUCache<int, string>(100);
            
            var stopwatch = Stopwatch.StartNew();
            
            // Заполняем кэш
            for (int i = 0; i < size; i++)
            {
                cache.Put(i, $"Value_{i}");
            }
            
            // Читаем из кэша
            for (int i = 0; i < size; i++)
            {
                cache.Get(i % 100); // Доступ к элементам в кэше
            }
            
            stopwatch.Stop();
            
            sizes.Add(size);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Операций: {size}, Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
        }
        
        CreatePerformancePlot("LRU Cache", sizes, times, "Количество операций", "Время (мс)", "lru_cache_performance.png");
    }

    private void BenchmarkBracketParser()
    {
        Console.WriteLine("\n--- Bracket Parser Performance ---");
        
        var sizes = new List<int>();
        var times = new List<double>();
        
        for (int size = 100; size <= 5000; size += 200)
        {
            // Генерируем строку с вложенными скобками
            string expression = GenerateNestedBrackets(size);
            
            var stopwatch = Stopwatch.StartNew();
            BracketParser.IsBalanced(expression);
            stopwatch.Stop();
            
            sizes.Add(size);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Длина: {size}, Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
        }
        
        CreatePerformancePlot("Bracket Parser", sizes, times, "Длина выражения", "Время (мс)", "bracket_parser_performance.png");
    }

    private void BenchmarkGraphBFS()
    {
        Console.WriteLine("\n--- Graph BFS Performance ---");
        
        var sizes = new List<int>();
        var times = new List<double>();
        
        for (int vertices = 50; vertices <= 1000; vertices += 50)
        {
            var graph = new GraphBFS();
            
            // Создаем граф с заданным количеством вершин
            for (int i = 0; i < vertices - 1; i++)
            {
                graph.AddEdge(i, i + 1);
            }
            
            // Добавляем случайные связи
            var random = new Random();
            for (int i = 0; i < vertices / 2; i++)
            {
                int from = random.Next(vertices);
                int to = random.Next(vertices);
                if (from != to)
                {
                    graph.AddEdge(from, to);
                }
            }
            
            var stopwatch = Stopwatch.StartNew();
            graph.BFS(0);
            stopwatch.Stop();
            
            sizes.Add(vertices);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Вершин: {vertices}, Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
        }
        
        CreatePerformancePlot("Graph BFS", sizes, times, "Количество вершин", "Время (мс)", "graph_bfs_performance.png");
    }

    private void BenchmarkTrieAutocomplete()
    {
        Console.WriteLine("\n--- Trie Autocomplete Performance ---");
        
        var sizes = new List<int>();
        var times = new List<double>();
        
        for (int wordCount = 100; wordCount <= 5000; wordCount += 200)
        {
            var trie = new TrieAutocomplete();
            
            // Генерируем словарь
            var words = GenerateWordList(wordCount);
            
            var stopwatch = Stopwatch.StartNew();
            
            // Вставляем слова
            foreach (string word in words)
            {
                trie.Insert(word);
            }
            
            // Ищем автокомплиты
            for (int i = 0; i < 100; i++)
            {
                string prefix = words[i % words.Count].Substring(0, Math.Min(3, words[i % words.Count].Length));
                trie.Search(prefix);
            }
            
            stopwatch.Stop();
            
            sizes.Add(wordCount);
            times.Add(stopwatch.Elapsed.TotalMilliseconds);
            
            Console.WriteLine($"Слов: {wordCount}, Время: {stopwatch.Elapsed.TotalMilliseconds:F2} мс");
        }
        
        CreatePerformancePlot("Trie Autocomplete", sizes, times, "Количество слов", "Время (мс)", "trie_autocomplete_performance.png");
    }

    private string GenerateNestedBrackets(int depth)
    {
        var result = new System.Text.StringBuilder();
        var random = new Random();
        
        for (int i = 0; i < depth; i++)
        {
            char[] brackets = { '(', '[', '{', '<' };
            char[] closingBrackets = { ')', ']', '}', '>' };
            
            int bracketIndex = random.Next(brackets.Length);
            result.Append(brackets[bracketIndex]);
        }
        
        for (int i = 0; i < depth; i++)
        {
            result.Append(')');
        }
        
        return result.ToString();
    }

    private List<string> GenerateWordList(int count)
    {
        var words = new List<string>();
        var random = new Random();
        string[] prefixes = { "auto", "data", "info", "tech", "code", "test", "demo", "user", "admin", "system" };
        string[] suffixes = { "tion", "ment", "ing", "er", "ly", "al", "ic", "ed", "or", "ry" };
        
        for (int i = 0; i < count; i++)
        {
            string prefix = prefixes[random.Next(prefixes.Length)];
            string suffix = suffixes[random.Next(suffixes.Length)];
            string word = prefix + suffix + i.ToString();
            words.Add(word);
        }
        
        return words;
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
