public class CustomQueue<T>
{
    private Node<T> _front; // начало очереди
    private Node<T> _rear;  // конец очереди

    public CustomQueue()
    {
        _front = null;
        _rear = null;
    }

    // Вставка элемента в конец очереди
    public void Enqueue(T value)
    {
        Node<T> newNode = new Node<T>(value);
        
        if (_rear == null)
        {
            _front = _rear = newNode;
        }
        else
        {
            _rear.next = newNode;
            _rear = newNode;
        }
    }

    // Удаление элемента из начала очереди
    public T Dequeue()
    {
        if (_front == null)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        T value = _front.data;
        _front = _front.next;
        
        if (_front == null)
        {
            _rear = null;
        }
        
        return value;
    }

    // Просмотр первого элемента без удаления
    public T Peek()
    {
        if (_front == null)
        {
            throw new InvalidOperationException("Queue is empty");
        }
        
        return _front.data;
    }

    // Проверка на пустоту
    public bool IsEmpty()
    {
        return _front == null;
    }

    // Печать очереди
    public void Print()
    {
        if (_front == null)
        {
            Console.WriteLine("Queue is empty");
            return;
        }

        var current = _front;
        var n = 1;
        while (current != null)
        {
            Console.WriteLine($"{n} элемент - {current.data}");
            n++;
            current = current.next;
        }
    }

    // Очистка очереди
    public void Clear()
    {
        _front = null;
        _rear = null;
    }

    // Подсчет элементов
    public int Count()
    {
        int count = 0;
        var current = _front;
        while (current != null)
        {
            current = current.next;
            count++;
        }
        return count;
    }
}
