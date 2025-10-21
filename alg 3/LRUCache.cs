// Пример применения Списка: LRU (Least Recently Used) Cache
// Применение: кэширование в веб-серверах, базах данных, операционных системах
public class LRUCache<TKey, TValue> where TKey : notnull
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, CacheNode<TKey, TValue>> _cache;
    private readonly DoublyLinkedList<TKey, TValue> _list;

    public LRUCache(int capacity)
    {
        _capacity = capacity;
        _cache = new Dictionary<TKey, CacheNode<TKey, TValue>>();
        _list = new DoublyLinkedList<TKey, TValue>();
    }

    public TValue? Get(TKey key)
    {
        if (_cache.TryGetValue(key, out var node))
        {
            _list.MoveToFront(node);
            return node.Value;
        }
        return default(TValue);
    }

    public void Put(TKey key, TValue value)
    {
        if (_cache.TryGetValue(key, out var existingNode))
        {
            existingNode.Value = value;
            _list.MoveToFront(existingNode);
        }
        else
        {
            if (_cache.Count >= _capacity)
            {
                var lastNode = _list.RemoveLast();
                if (lastNode != null)
                {
                    _cache.Remove(lastNode.Key);
                }
            }

            var newNode = new CacheNode<TKey, TValue>(key, value);
            _list.AddToFront(newNode);
            _cache[key] = newNode;
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
        _list.Print();
    }
}

public class CacheNode<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
    public CacheNode<TKey, TValue>? Next { get; set; }
    public CacheNode<TKey, TValue>? Prev { get; set; }

    public CacheNode(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}

public class DoublyLinkedList<TKey, TValue>
{
    private CacheNode<TKey, TValue>? _head;
    private CacheNode<TKey, TValue>? _tail;

    public void AddToFront(CacheNode<TKey, TValue> node)
    {
        node.Next = _head;
        node.Prev = null;

        if (_head != null)
        {
            _head.Prev = node;
        }

        _head = node;

        if (_tail == null)
        {
            _tail = node;
        }
    }

    public void MoveToFront(CacheNode<TKey, TValue> node)
    {
        if (_head == node) return;

        if (node.Prev != null)
        {
            node.Prev.Next = node.Next;
        }

        if (node.Next != null)
        {
            node.Next.Prev = node.Prev;
        }

        if (_tail == node)
        {
            _tail = node.Prev;
        }

        AddToFront(node);
    }

    public CacheNode<TKey, TValue>? RemoveLast()
    {
        if (_tail == null) return null;

        var lastNode = _tail;

        if (_tail.Prev != null)
        {
            _tail.Prev.Next = null;
            _tail = _tail.Prev;
        }
        else
        {
            _head = null;
            _tail = null;
        }

        return lastNode;
    }

    public void Print()
    {
        var current = _head;
        int index = 1;
        while (current != null)
        {
            Console.WriteLine($"{index}. {current.Key} = {current.Value}");
            current = current.Next;
            index++;
        }
    }
}
