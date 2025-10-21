// Пример применения Стека: Парсер скобок с проверкой баланса
// Применение: компиляторы, IDE, валидация JSON/XML, калькуляторы
public class BracketParser
{
    private static readonly Dictionary<char, char> BracketPairs = new Dictionary<char, char>
    {
        { '(', ')' },
        { '[', ']' },
        { '{', '}' },
        { '<', '>' }
    };

    public static bool IsBalanced(string expression)
    {
        var stack = new System.Collections.Generic.Stack<char>();
        
        foreach (char c in expression)
        {
            if (IsOpeningBracket(c))
            {
                stack.Push(c);
            }
            else if (IsClosingBracket(c))
            {
                if (stack.Count == 0)
                    return false;
                
                char opening = stack.Pop();
                if (!AreMatchingBrackets(opening, c))
                    return false;
            }
        }
        
        return stack.Count == 0;
    }

    public static string FindUnbalancedBrackets(string expression)
    {
        var stack = new System.Collections.Generic.Stack<(char bracket, int position)>();
        var errors = new List<string>();
        
        for (int i = 0; i < expression.Length; i++)
        {
            char c = expression[i];
            
            if (IsOpeningBracket(c))
            {
                stack.Push((c, i));
            }
            else if (IsClosingBracket(c))
            {
                if (stack.Count == 0)
                {
                    errors.Add($"Неожиданная закрывающая скобка '{c}' в позиции {i}");
                }
                else
                {
                    var (opening, pos) = stack.Pop();
                    if (!AreMatchingBrackets(opening, c))
                    {
                        errors.Add($"Несоответствие скобок: '{opening}' в позиции {pos} и '{c}' в позиции {i}");
                    }
                }
            }
        }
        
        // Проверяем оставшиеся открывающие скобки
        while (stack.Count > 0)
        {
            var (bracket, pos) = stack.Pop();
            errors.Add($"Незакрытая скобка '{bracket}' в позиции {pos}");
        }
        
        return string.Join("\n", errors);
    }

    private static bool IsOpeningBracket(char c)
    {
        return BracketPairs.ContainsKey(c);
    }

    private static bool IsClosingBracket(char c)
    {
        return BracketPairs.Values.Contains(c);
    }

    private static bool AreMatchingBrackets(char opening, char closing)
    {
        return BracketPairs.TryGetValue(opening, out char expectedClosing) && 
               expectedClosing == closing;
    }

    public static void Demonstrate()
    {
        string[] testCases = {
            "()",
            "()[]{}",
            "([{}])",
            "((()))",
            "([)]",
            "(((",
            ")))",
            "{[()]}",
            "{[()]",
            "{[()]}"
        };

        Console.WriteLine("=== Демонстрация парсера скобок ===");
        foreach (var test in testCases)
        {
            bool isBalanced = IsBalanced(test);
            Console.WriteLine($"'{test}' - {(isBalanced ? "Сбалансировано" : "НЕ сбалансировано")}");
            
            if (!isBalanced)
            {
                string errors = FindUnbalancedBrackets(test);
                Console.WriteLine($"  Ошибки: {errors}");
            }
        }
    }
}
