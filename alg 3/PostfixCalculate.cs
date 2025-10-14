public class PostfixCalculate
{
    private string[] _operations = ["+", "-", "*", "/", "^", "ln", "cos", "sin", "sqrt"];
    
    private Stack<string> _stack = new Stack<string>();

    private List<string> TakeFromFile()
    {
        string text = File.ReadAllText("postfix.txt");
        // Разбиваем по пробелам, а не посимвольно
        return text.Split(new[] { ' ', '\t', '\n', '\r' }, 
                         StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public double Calculate()
    {
        var elements = TakeFromFile();

        foreach (var element in elements)
        {
            if (double.TryParse(element, out _)) // Проверяем, число ли это
            {
                _stack.Push(element);
            }
            else if (_operations.Contains(element))
            {
                switch (element)
                {
                    case "+":
                        _stack.Push(OperatorAdd().ToString());
                        break;
                    case "-":
                        _stack.Push(OperatorMinus().ToString());
                        break;
                    case "*":
                        _stack.Push(OperatorMultiply().ToString());
                        break;
                    case "/":
                        _stack.Push(OperatorDivide().ToString());
                        break;
                    case "^":
                        _stack.Push(OperatorDegree().ToString());
                        break;
                    case "ln":
                        _stack.Push(OperatorLog().ToString());
                        break;
                    case "cos":
                        _stack.Push(OperatorCos().ToString());
                        break;
                    case "sin":
                        _stack.Push(OperatorSin().ToString());
                        break;
                    case "sqrt":
                        _stack.Push(OperatorSqrt().ToString());
                        break;
                }
            }
            
        }
        
        
        return double.Parse(_stack.Pop()); // Используем Pop() вместо Top()
    }

    private double OperatorAdd()
    {
        double second = double.Parse(_stack.Pop());
        double first = double.Parse(_stack.Pop());
        return first + second;
    }

    private double OperatorMinus()
    {
        double second = double.Parse(_stack.Pop());
        double first = double.Parse(_stack.Pop());
        return first - second;
    }

    private double OperatorMultiply()
    {
        double second = double.Parse(_stack.Pop());
        double first = double.Parse(_stack.Pop());
        return first * second;
    }

    private double OperatorDivide()
    {
        double second = double.Parse(_stack.Pop());
        double first = double.Parse(_stack.Pop());
        return first / second;
    }

    private double OperatorDegree()
    {
        double second = double.Parse(_stack.Pop());
        double first = double.Parse(_stack.Pop());
        return Math.Pow(first, second);
    }

    private double OperatorLog()
    {
        double first = double.Parse(_stack.Pop());
        return Math.Log(first);
    }

    private double OperatorCos()
    {
        double value = double.Parse(_stack.Pop());
        return Math.Cos(value);
    }
    
    private double OperatorSin()
    {
        double value = double.Parse(_stack.Pop());
        return Math.Sin(value);
    }

    private double OperatorSqrt()
    {
        double value = double.Parse(_stack.Pop());
        return Math.Sqrt(value);
    }
}