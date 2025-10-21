public class QueueOperation
{
    private string[] GetOperation()
    {
        string line = File.ReadAllText("input.txt");
        return line.Split(' ');
    }

    public void MakeOperationWithCustomQueue()
    {
        var queue = new CustomQueue<string>();
        var operations = GetOperation();

        Console.WriteLine("=== Работа с собственной реализацией очереди ===");
        foreach (var operation in operations)
        {
            if (operation.StartsWith("1,"))
            {
                string value = operation.Substring(2);
                queue.Enqueue(value);
                Console.WriteLine($"В очередь добавлено: {value}");
            }
            else if (operation.StartsWith("2"))
            {
                if (!queue.IsEmpty())
                {
                    Console.WriteLine($"Удалено из очереди: {queue.Dequeue()}");
                }
                else
                {
                    Console.WriteLine("Очередь пуста, удалять нечего");
                }
            }
            else if (operation.StartsWith("3"))
            {
                if (!queue.IsEmpty())
                {
                    Console.WriteLine($"Первый элемент очереди: {queue.Peek()}");
                }
                else
                {
                    Console.WriteLine("Очередь пуста, первого элемента нет");
                }
            }
            else if (operation.StartsWith("4"))
            {
                Console.WriteLine($"Очередь пуста? - {queue.IsEmpty()}");
            }
            else if (operation.StartsWith("5"))
            {
                Console.WriteLine("Очередь:");
                queue.Print();
            }
        }
    }

    public void MakeOperationWithStandardQueue()
    {
        var queue = new Queue<string>();
        var operations = GetOperation();

        Console.WriteLine("=== Работа со стандартной реализацией Queue ===");
        foreach (var operation in operations)
        {
            if (operation.StartsWith("1,"))
            {
                string value = operation.Substring(2);
                queue.Enqueue(value);
                Console.WriteLine($"В очередь добавлено: {value}");
            }
            else if (operation.StartsWith("2"))
            {
                if (queue.Count > 0)
                {
                    Console.WriteLine($"Удалено из очереди: {queue.Dequeue()}");
                }
                else
                {
                    Console.WriteLine("Очередь пуста, удалять нечего");
                }
            }
            else if (operation.StartsWith("3"))
            {
                if (queue.Count > 0)
                {
                    Console.WriteLine($"Первый элемент очереди: {queue.Peek()}");
                }
                else
                {
                    Console.WriteLine("Очередь пуста, первого элемента нет");
                }
            }
            else if (operation.StartsWith("4"))
            {
                Console.WriteLine($"Очередь пуста? - {queue.Count == 0}");
            }
            else if (operation.StartsWith("5"))
            {
                Console.WriteLine("Очередь:");
                if (queue.Count == 0)
                {
                    Console.WriteLine("Queue is empty");
                }
                else
                {
                    var items = queue.ToArray();
                    for (int i = 0; i < items.Length; i++)
                    {
                        Console.WriteLine($"{i + 1} элемент - {items[i]}");
                    }
                }
            }
        }
    }
}
