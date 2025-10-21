// Алгоритмы работы со связными списками
public class LinkedListAlgorithms
{
    // 1. Функция переворота списка
    public static Node<T> ReverseList<T>(Node<T> head)
    {
        Node<T> prev = null;
        Node<T> current = head;
        Node<T> next = null;

        while (current != null)
        {
            next = current.next;
            current.next = prev;
            prev = current;
            current = next;
        }

        return prev;
    }

    // 2. Перенос последнего элемента в начало
    public static Node<T> MoveLastToFirst<T>(Node<T> head)
    {
        if (head == null || head.next == null)
            return head;

        Node<T> current = head;
        Node<T> prev = null;

        // Находим последний элемент
        while (current.next != null)
        {
            prev = current;
            current = current.next;
        }

        // Переносим последний в начало
        prev.next = null;
        current.next = head;
        return current;
    }

    // 2. Перенос первого элемента в конец
    public static Node<T> MoveFirstToLast<T>(Node<T> head)
    {
        if (head == null || head.next == null)
            return head;

        Node<T> first = head;
        Node<T> current = head;

        // Находим последний элемент
        while (current.next != null)
        {
            current = current.next;
        }

        // Переносим первый в конец
        head = head.next;
        current.next = first;
        first.next = null;

        return head;
    }

    // 3. Подсчет количества различных элементов
    public static int CountDistinctElements(Node<int> head)
    {
        var distinctElements = new HashSet<int>();
        Node<int> current = head;

        while (current != null)
        {
            distinctElements.Add(current.data);
            current = current.next;
        }

        return distinctElements.Count;
    }

    // 4. Удаление неуникальных элементов (оставляем только уникальные)
    public static Node<T> RemoveNonUniqueElements<T>(Node<T> head)
    {
        if (head == null) return null;

        // Подсчитываем частоту каждого элемента
        var frequency = new Dictionary<T, int>();
        Node<T> current = head;

        while (current != null)
        {
            if (frequency.ContainsKey(current.data))
                frequency[current.data]++;
            else
                frequency[current.data] = 1;
            current = current.next;
        }

        // Удаляем элементы с частотой > 1
        Node<T> dummy = new Node<T>(default(T));
        dummy.next = head;
        Node<T> prev = dummy;
        current = head;

        while (current != null)
        {
            if (frequency[current.data] > 1)
            {
                prev.next = current.next;
                current = current.next;
            }
            else
            {
                prev = current;
                current = current.next;
            }
        }

        return dummy.next;
    }

    // 5. Вставка списка в себя после первого вхождения x
    public static Node<T> InsertListAfterFirstOccurrence<T>(Node<T> head, T x)
    {
        if (head == null) return null;

        // Находим первое вхождение x
        Node<T> target = head;
        while (target != null && !target.data.Equals(x))
        {
            target = target.next;
        }

        if (target == null) return head; // x не найден

        // Создаем копию списка
        Node<T> copyHead = CopyList(head);
        
        // Вставляем копию после target
        Node<T> temp = target.next;
        target.next = copyHead;
        
        // Находим конец копии и связываем с остатком
        Node<T> copyTail = copyHead;
        while (copyTail.next != null)
        {
            copyTail = copyTail.next;
        }
        copyTail.next = temp;

        return head;
    }

    // 6. Вставка элемента в упорядоченный список
    public static Node<int> InsertInSortedList(Node<int> head, int element)
    {
        Node<int> newNode = new Node<int>(element);

        // Если список пуст или элемент меньше первого
        if (head == null || element <= head.data)
        {
            newNode.next = head;
            return newNode;
        }

        Node<int> current = head;
        while (current.next != null && current.next.data < element)
        {
            current = current.next;
        }

        newNode.next = current.next;
        current.next = newNode;

        return head;
    }

    // 7. Удаление всех вхождений элемента E
    public static Node<T> RemoveAllOccurrences<T>(Node<T> head, T element)
    {
        Node<T> dummy = new Node<T>(default(T));
        dummy.next = head;
        Node<T> prev = dummy;
        Node<T> current = head;

        while (current != null)
        {
            if (current.data.Equals(element))
            {
                prev.next = current.next;
                current = current.next;
            }
            else
            {
                prev = current;
                current = current.next;
            }
        }

        return dummy.next;
    }

    // 8. Вставка элемента F перед первым вхождением E
    public static Node<T> InsertBeforeFirstOccurrence<T>(Node<T> head, T elementToInsert, T targetElement)
    {
        Node<T> newNode = new Node<T>(elementToInsert);

        // Если список пуст
        if (head == null) return null;

        // Если первый элемент - target
        if (head.data.Equals(targetElement))
        {
            newNode.next = head;
            return newNode;
        }

        Node<T> current = head;
        while (current.next != null && !current.next.data.Equals(targetElement))
        {
            current = current.next;
        }

        if (current.next != null) // target найден
        {
            newNode.next = current.next;
            current.next = newNode;
        }

        return head;
    }

    // 9. Дописывание списка E к списку L
    public static Node<T> AppendList<T>(Node<T> listL, Node<T> listE)
    {
        if (listL == null) return listE;
        if (listE == null) return listL;

        Node<T> current = listL;
        while (current.next != null)
        {
            current = current.next;
        }

        current.next = listE;
        return listL;
    }

    // 10. Разбиение списка на два по первому вхождению заданного числа
    public static (Node<T> firstList, Node<T> secondList) SplitList<T>(Node<T> head, T splitValue)
    {
        if (head == null) return (null, null);

        Node<T> dummy = new Node<T>(default(T));
        dummy.next = head;
        Node<T> prev = dummy;
        Node<T> current = head;

        while (current != null && !current.data.Equals(splitValue))
        {
            prev = current;
            current = current.next;
        }

        if (current == null) // splitValue не найден
        {
            return (head, null);
        }

        // Разделяем списки
        Node<T> secondList = current.next;
        current.next = null;

        return (dummy.next, secondList);
    }

    // 11. Удвоение списка (приписывание себя к концу)
    public static Node<T> DoubleList<T>(Node<T> head)
    {
        if (head == null) return null;

        Node<T> copy = CopyList(head);
        Node<T> current = head;

        while (current.next != null)
        {
            current = current.next;
        }

        current.next = copy;
        return head;
    }

    // 12. Поменять местами два элемента списка
    public static Node<T> SwapElements<T>(Node<T> head, T element1, T element2)
    {
        if (head == null || element1.Equals(element2)) return head;

        Node<T> dummy = new Node<T>(default(T));
        dummy.next = head;
        Node<T> prev1 = null, prev2 = null;
        Node<T> node1 = null, node2 = null;
        Node<T> current = head;
        Node<T> prev = dummy;

        // Находим элементы и их предыдущие узлы
        while (current != null)
        {
            if (current.data.Equals(element1))
            {
                node1 = current;
                prev1 = prev;
            }
            else if (current.data.Equals(element2))
            {
                node2 = current;
                prev2 = prev;
            }

            prev = current;
            current = current.next;
        }

        if (node1 == null || node2 == null) return head; // Один из элементов не найден

        // Меняем местами
        if (node1.next == node2) // Соседние элементы
        {
            prev1.next = node2;
            node1.next = node2.next;
            node2.next = node1;
        }
        else if (node2.next == node1) // Соседние элементы (обратный порядок)
        {
            prev2.next = node1;
            node2.next = node1.next;
            node1.next = node2;
        }
        else // Не соседние элементы
        {
            Node<T> temp = node1.next;
            prev1.next = node2;
            prev2.next = node1;
            node1.next = node2.next;
            node2.next = temp;
        }

        return dummy.next;
    }

    // Вспомогательная функция для копирования списка
    private static Node<T> CopyList<T>(Node<T> head)
    {
        if (head == null) return null;

        Node<T> newHead = new Node<T>(head.data);
        Node<T> current = newHead;
        Node<T> original = head.next;

        while (original != null)
        {
            current.next = new Node<T>(original.data);
            current = current.next;
            original = original.next;
        }

        return newHead;
    }

    // Вспомогательная функция для создания списка из массива
    public static Node<T> CreateListFromArray<T>(T[] array)
    {
        if (array == null || array.Length == 0) return null;

        Node<T> head = new Node<T>(array[0]);
        Node<T> current = head;

        for (int i = 1; i < array.Length; i++)
        {
            current.next = new Node<T>(array[i]);
            current = current.next;
        }

        return head;
    }

    // Вспомогательная функция для печати списка
    public static void PrintList<T>(Node<T> head, string title = "Список")
    {
        Console.WriteLine($"{title}:");
        if (head == null)
        {
            Console.WriteLine("(пустой)");
            return;
        }

        Node<T> current = head;
        while (current != null)
        {
            Console.Write($"{current.data} ");
            current = current.next;
        }
        Console.WriteLine();
    }

    // Вспомогательная функция для преобразования списка в массив
    public static T[] ListToArray<T>(Node<T> head)
    {
        var list = new List<T>();
        Node<T> current = head;

        while (current != null)
        {
            list.Add(current.data);
            current = current.next;
        }

        return list.ToArray();
    }
}
