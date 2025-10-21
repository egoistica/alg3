public class StackOperation
{
    
    private string[] GetOperation()
    {
        string line = File.ReadAllText("input.txt");
        return line.Split(' ');
    }

    public void MakeOperation()
    {
        var stack = new Stack<string>();
        var operations = GetOperation();

        foreach (var operation in operations)
        {
            if (operation.StartsWith("1,"))
            {
                string number = operation.Substring(2);
                stack.Push(number);
                Console.WriteLine($"В стэк добавлено {number}");
            }
            else if (operation.StartsWith("2"))
            {
                if (!stack.IsEmpty())
                {
                    Console.WriteLine($"Удалено: {stack.Pop()}");
                }
                else
                {
                    Console.WriteLine("Стэк пуст, Pop-ить нечего");
                }
            }
            
            else if (operation.StartsWith("3"))
            {
                if (!stack.IsEmpty())
                {
                    Console.WriteLine($"Верхний элемент: {stack.Top()}");
                }
                else
                {
                    Console.WriteLine("Стэк пуст, Top-а нет");
                }
                
            }
            
            else if (operation.StartsWith("4"))
            {
                Console.WriteLine($"Стэк пуст? - {stack.IsEmpty()}");
            }
            
            else if (operation.StartsWith("5"))
            {
                Console.WriteLine("Стэк:");
                stack.Print();
            }
        }
    }
}
