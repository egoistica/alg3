// Пример применения Очереди: BFS (Breadth-First Search) обход графа
// Применение: поиск кратчайшего пути, анализ социальных сетей, поиск в ширину
public class GraphBFS
{
    private Dictionary<int, List<int>> _adjacencyList;

    public GraphBFS()
    {
        _adjacencyList = new Dictionary<int, List<int>>();
    }

    public void AddEdge(int from, int to)
    {
        if (!_adjacencyList.ContainsKey(from))
            _adjacencyList[from] = new List<int>();
        if (!_adjacencyList.ContainsKey(to))
            _adjacencyList[to] = new List<int>();
            
        _adjacencyList[from].Add(to);
        _adjacencyList[to].Add(from); // Для неориентированного графа
    }

    public List<int> BFS(int startVertex)
    {
        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        var result = new List<int>();

        queue.Enqueue(startVertex);
        visited.Add(startVertex);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            result.Add(current);

            if (_adjacencyList.ContainsKey(current))
            {
                foreach (int neighbor in _adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return result;
    }

    public Dictionary<int, int> ShortestPath(int startVertex)
    {
        var distances = new Dictionary<int, int>();
        var queue = new Queue<int>();
        var visited = new HashSet<int>();

        queue.Enqueue(startVertex);
        visited.Add(startVertex);
        distances[startVertex] = 0;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();

            if (_adjacencyList.ContainsKey(current))
            {
                foreach (int neighbor in _adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        distances[neighbor] = distances[current] + 1;
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return distances;
    }

    public void PrintGraph()
    {
        Console.WriteLine("Структура графа:");
        foreach (var kvp in _adjacencyList)
        {
            Console.WriteLine($"Вершина {kvp.Key}: [{string.Join(", ", kvp.Value)}]");
        }
    }

    public static void Demonstrate()
    {
        var graph = new GraphBFS();
        
        // Создаем граф: 0-1-2-3-4
        //              |
        //              5
        graph.AddEdge(0, 1);
        graph.AddEdge(1, 2);
        graph.AddEdge(2, 3);
        graph.AddEdge(3, 4);
        graph.AddEdge(1, 5);

        Console.WriteLine("=== Демонстрация BFS ===");
        graph.PrintGraph();

        Console.WriteLine("\nBFS обход начиная с вершины 0:");
        var bfsResult = graph.BFS(0);
        Console.WriteLine($"Порядок посещения: {string.Join(" -> ", bfsResult)}");

        Console.WriteLine("\nКратчайшие расстояния от вершины 0:");
        var distances = graph.ShortestPath(0);
        foreach (var kvp in distances.OrderBy(x => x.Key))
        {
            Console.WriteLine($"До вершины {kvp.Key}: {kvp.Value} шагов");
        }
    }
}
