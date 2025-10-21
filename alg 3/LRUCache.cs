// Пример применения Списка: LRU (Least Recently Used) Cache
// Применение: кэширование в веб-серверах, базах данных, операционных системах
public class LRUCache<TKey, TValue>
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<CacheItem<TKey, TValue>>> _cache;
    private readonly LinkedList<CacheItem<TKey, TValue>> _accessOrder;

    public LRUCache(int capacity)
    {
        _capacity = capacity;
        _cache = new Dictionary<TKey, LinkedListNode<CacheItem<TKey, TValue>>>();
        _accessOrder = new LinkedList<CacheItem<TKey, TValue>>();
    }

    public TValue Get(TKey key)
    {
        if (_cache.TryGetValue(key, out var node))
        {
            // Перемещаем узел в начало (самый недавно использованный)
            _accessOrder.Remove(node);
            _accessOrder.AddFirst(node);
            return node.Value.Value;
        }
        return default(TValue);
    }

    public void Put(TKey key, TValue value)
    {
        if (_cache.TryGetValue(key, out var existingNode))
        {
            // Обновляем существующий элемент
            existingNode.Value.Value = value;
            _accessOrder.Remove(existingNode);
            _accessOrder.AddFirst(existingNode);
        }
        else
        {
            // Добавляем новый элемент
            if (_cache.Count >= _capacity)
            {
                // Удаляем самый старый элемент (последний в списке)
                var lastNode = _accessOrder.Last;
                _cache.Remove(lastNode.Value.Key);
                _accessOrder.Remove(lastNode);
            }

            var newNode = new LinkedListNode<CacheItem<TKey, TValue>>(
                new CacheItem<TKey, TValue> { Key = key, Value = value });
            _cache[key] = newNode;
            _accessOrder.AddFirst(newNode);
        }
    }

    public bool ContainsKey(TKey key)
    {
        return _cache.ContainsKey(key);
    }

    public int Count => _cache.Count;

    public void PrintCache()
    {
        Console.WriteLine("LRU Cache содержимое (от недавнего к старому):");
        var current = _accessOrder.First;
        int index = 1;
        while (current != null)
        {
            Console.WriteLine($"{index}. {current.Value.Key} = {current.Value.Value}");
            current = current.Next;
            index++;
        }
    }
}

public class CacheItem<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
}
