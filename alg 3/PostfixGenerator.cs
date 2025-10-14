public class PostfixFileGenerator
{
    private Random _random = new Random();
    
    private string[] _binaryOperators = { "+", "-", "*", "/", "^" };
    private string[] _unaryOperators = { "ln", "cos", "sin", "sqrt" };
    
    // Основной метод для генерации постфиксного выражения с заданным количеством символов
    public void GeneratePostfixBySymbolCount(int symbolCount, string filename = "postfix.txt")
    {
        var tokens = new List<string>();
        int stackDepth = 0;
        int currentSymbolCount = 0;
        int lastReportedProgress = -1;
        
        Console.WriteLine($"Начинаем генерацию выражения с {symbolCount} символами...");
        
        while (currentSymbolCount < symbolCount)
        {
            // Решаем, что добавить: число или оператор
            if (ShouldAddNumber(stackDepth, currentSymbolCount, symbolCount))
            {
                string number = GenerateRandomNumber();
                tokens.Add(number);
                currentSymbolCount += number.Length + (tokens.Count > 1 ? 1 : 0); // +1 для пробела, кроме первого элемента
                stackDepth++;
            }
            else if (stackDepth >= GetRequiredOperandsForRandomOperator())
            {
                string op = GenerateRandomOperator();
                tokens.Add(op);
                currentSymbolCount += op.Length + 1; // +1 для пробела
                
                // Обновляем глубину стека в зависимости от типа оператора
                if (IsBinaryOperator(op))
                {
                    stackDepth--; // Бинарный оператор: берет 2, возвращает 1
                }
                // Унарный оператор: берет 1, возвращает 1 - глубина не меняется
            }
            
            // Прогресс каждые 10% (без деления на ноль)
            if (symbolCount > 0)
            {
                int progress = (currentSymbolCount * 100) / symbolCount;
                if (progress > lastReportedProgress && progress % 10 == 0)
                {
                    Console.WriteLine($"Прогресс: {progress}% ({currentSymbolCount} / {symbolCount} символов)");
                    lastReportedProgress = progress;
                }
            }
        }
        
        // Завершаем выражение, добавляя оставшиеся операторы
        while (stackDepth > 1)
        {
            string op = GenerateRandomBinaryOperator();
            tokens.Add(op);
            currentSymbolCount += op.Length + 1;
            stackDepth--;
        }
        
        string expression = string.Join(" ", tokens);
        File.WriteAllText(filename, expression);
        
        Console.WriteLine($"Файл сгенерирован: {filename}");
        Console.WriteLine($"Фактическое количество символов: {expression.Length}");
        Console.WriteLine($"Количество токенов: {tokens.Count}");
        
        // Показываем только если выражение не слишком длинное
        if (expression.Length <= 150)
        {
            Console.WriteLine($"Выражение: {expression}");
        }
        else
        {
            Console.WriteLine($"Начало выражения: {expression.Substring(0, 100)}...");
            Console.WriteLine($"Конец выражения: ...{expression.Substring(expression.Length - 50)}");
        }
    }
    
    private bool ShouldAddNumber(int stackDepth, int currentSymbolCount, int targetSymbolCount)
    {
        // Если стек почти пуст - добавляем число
        if (stackDepth <= 1) return true;
        
        // Если достигли целевого размера - добавляем операторы для завершения
        if (currentSymbolCount >= targetSymbolCount * 0.95) return false;
        
        // Случайный выбор с учетом текущей глубины стека
        return _random.NextDouble() < 0.6; // 60% chance для чисел
    }
    
    private string GenerateRandomNumber()
    {
        // Генерируем числа разной длины для большего разнообразия
        int numberType = _random.Next(0, 100);
        
        if (numberType < 40) // 40% - маленькие числа (1-2 цифры)
        {
            return _random.Next(1, 100).ToString();
        }
        else if (numberType < 70) // 30% - средние числа (3-4 цифры)
        {
            return _random.Next(100, 10000).ToString();
        }
        else if (numberType < 90) // 20% - большие числа (5-6 цифр)
        {
            return _random.Next(10000, 1000000).ToString();
        }
        else // 10% - вещественные числа
        {
            return (_random.NextDouble() * 1000).ToString("F3");
        }
    }
    
    private string GenerateRandomOperator()
    {
        return _random.NextDouble() < 0.7 ? GenerateRandomBinaryOperator() : GenerateRandomUnaryOperator();
    }
    
    private string GenerateRandomBinaryOperator()
    {
        return _binaryOperators[_random.Next(_binaryOperators.Length)];
    }
    
    private string GenerateRandomUnaryOperator()
    {
        return _unaryOperators[_random.Next(_unaryOperators.Length)];
    }
    
    private int GetRequiredOperandsForRandomOperator()
    {
        return _random.NextDouble() < 0.7 ? 2 : 1;
    }
    
    private bool IsBinaryOperator(string op)
    {
        return _binaryOperators.Contains(op);
    }
}