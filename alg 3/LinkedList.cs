public class LinkedList<T>
{
    public Node<T> head;

    public LinkedList()
    {
        this.head = null;
    }

    public void AddFirst(T value)
    {
        var newNode = new Node<T>(value);
        newNode.next = this.head;
        this.head = newNode;
    }

    public void AddLast(T value)
    {
        var newNode = new Node<T>(value);
        if (this.head == null)
        {
            this.head = newNode;
        }

        else
        {
            var current = this.head;
            while (current.next != null)
            {
                current = current.next;
            }
            current.next = newNode;
        }
    }

    public bool Contains(T value)
    {
        var current = this.head;
        while (current != null)
        {
            if (object.Equals(current.data, value))
            {
                return true;
            }
            current = current.next;
        }
        return false;
    }

    public void Print()
    {
        var current = this.head;
        var n = 1;
        while (current != null)
        {
            Console.WriteLine($"{n} элемент - {current.data}");
            n++;
            current = current.next;
        }
    }
}