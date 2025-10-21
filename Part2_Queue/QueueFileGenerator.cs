public class QueueFileGenerator
{
    private Random _rand = new Random();
    
    private string[] _words = { "cat", "dog", "hello", "world", "queue", "data", "test", "benchmark", "performance", "algorithm" };
    
    public void GenerateFile(int size)
    { 
        var operations = new List<string>();
        
        for (int i = 0; i < size; i++)
        {
            RandomOperation(operations);
        }

        File.WriteAllText("input.txt", string.Join(" ", operations));
    }
    
    public void GenerateFileWithComposition(int size, int[] operationTypes)
    {
        var operations = new List<string>();
        
        for (int i = 0; i < size; i++)
        {
            int operationType = operationTypes[i % operationTypes.Length];
            AddOperation(operations, operationType);
        }

        File.WriteAllText("input.txt", string.Join(" ", operations));
    }

    private void RandomOperation(List<string> operations)
    {
        int operation = _rand.Next(1, 6);
        AddOperation(operations, operation);
    }
    
    private void AddOperation(List<string> operations, int operationType)
    {
        switch (operationType)
        {
            case 1:
                string operationString = "1,";
                operations.Add(RandomEnqueue(operationString));
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

    private string RandomEnqueue(string operationString)
    {
        if (_rand.Next(2) == 0)
        {
            return operationString + _rand.Next(1, 1000).ToString(); // числа от 1 до 999
        }
        else
        {
            return operationString + _words[_rand.Next(0, _words.Length)];
        }
    }
}
