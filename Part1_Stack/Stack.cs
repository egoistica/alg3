using System.Collections;

public class Stack<T>
{
    private Node<T> _top; // ссылка на вершину

    public Stack()
    {
        _top = null;
    }

    public void Push(T value)
    {
        Node<T> node = new Node<T>(value);
        var temp = _top;
        _top = node;
        _top.next = temp;
    }

    public T Pop()
    {
        if (_top == null)
        {
            throw new InvalidOperationException("Stack is empty");
        }

        else
        {
            var temp = _top;
            _top = _top.next;
            return temp.data;
        }
    }

    public T Top()
    {
        if (_top == null)
        {
            throw new InvalidOperationException("Stack is empty");
        }
        else
        {
            return _top.data;
        }
    }

    public bool IsEmpty()
    {
        return _top == null;
    }

    public void Print()
    {
        if (_top == null)
        {
            Console.WriteLine("Stack is empty");
            ;
        }
        else
        {
            var temp = _top;
            var n = 1;
            while (temp != null)
            {
                Console.WriteLine($"{n++} элемент - {temp.data}");
                temp = temp.next;
            }
        }
    }

    public void Clear()
    {
        _top = null;
    }

    public int Count()
    {
        int count = 0;
        var temp = _top;
        while (temp != null)
        {
            temp = temp.next;
            count++;
        }

        return count;
    }
    
}
