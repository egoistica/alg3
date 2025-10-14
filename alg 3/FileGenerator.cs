public class FileGenerator
{
    private Random _rand = new Random();
    
    private string[] _words = { "cat", "dog", "hello", "world", "stack", "queue", "tree", "graph", "data", "code" };
    
    public void GenerateFile(int size)
    { 
        var operations = new List<string>();
        
        for (int i = 0; i < size; i++)
        {
            RandomOperation(operations);
        }

        File.WriteAllText("input.txt", string.Join(" ", operations));
    }

    private void RandomOperation(List<string> operations)
    {
        int operation = _rand.Next(1, 6);

        switch (operation)
        {
            case 1:
                string operationString = "1,";
                operations.Add(RandomPush(operationString));
                break;
            case 2:
                operations.Add("2");
                break;
            case 3:
                operations.Add("3");
                break;
            case 4:
                operations.Add("4");
                break;
            case 5:
                operations.Add("5");
                break;
        }
        
    }

    private string RandomPush(string operationString)
    {
        if (_rand.Next(2) == 0)
        {
            return operationString + _rand.Next(1, 100).ToString(); // числа от 1 до 99
        }
        else
        {
            return operationString + _words[_rand.Next(0, _words.Length)];
        }
    }
    
}