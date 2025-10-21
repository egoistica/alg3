public class InfixToPostfix
{
    private static readonly Dictionary<string, int> Precedence = new Dictionary<string, int>
    {
        { "ln", 5 }, { "cos", 5 }, { "sin", 5 }, { "sqrt", 5 },
        { "^", 4 },
        { "*", 3 }, { "/", 3 },
        { "+", 2 }, { "-", 2 }
    };

    private static bool IsOperator(string token)
    {
        return Precedence.ContainsKey(token);
    }

    private static bool IsFunction(string token)
    {
        return token is "ln" or "cos" or "sin" or "sqrt";
    }

    private static bool IsRightAssociative(string token)
    {
        return token == "^"; // степень правоассоциативна
    }

    public string Convert(string infix)
    {
        var output = new List<string>();
        var stack = new Stack<string>();

        var tokens = Tokenize(infix);

        foreach (var token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                output.Add(token);
            }
            else if (IsFunction(token))
            {
                stack.Push(token);
            }
            else if (IsOperator(token))
            {
                while (!stack.IsEmpty() && IsOperator(stack.Top()) &&
                       ((Precedence[stack.Top()] > Precedence[token]) ||
                        (Precedence[stack.Top()] == Precedence[token] && !IsRightAssociative(token))))
                {
                    output.Add(stack.Pop());
                }
                stack.Push(token);
            }
            else if (token == "(")
            {
                stack.Push(token);
            }
            else if (token == ")")
            {
                while (!stack.IsEmpty() && stack.Top() != "(")
                {
                    output.Add(stack.Pop());
                }
                if (!stack.IsEmpty() && stack.Top() == "(")
                {
                    stack.Pop();
                }
                if (!stack.IsEmpty() && IsFunction(stack.Top()))
                {
                    output.Add(stack.Pop());
                }
            }
        }

        while (!stack.IsEmpty())
        {
            output.Add(stack.Pop());
        }

        return string.Join(" ", output);
    }

    private static List<string> Tokenize(string input)
    {
        var tokens = new List<string>();
        int i = 0;
        while (i < input.Length)
        {
            char c = input[i];
            if (char.IsWhiteSpace(c))
            {
                i++;
                continue;
            }
            if (char.IsDigit(c) || c == '.')
            {
                int start = i;
                while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.')) i++;
                tokens.Add(input.Substring(start, i - start));
                continue;
            }
            if (char.IsLetter(c))
            {
                int start = i;
                while (i < input.Length && char.IsLetter(input[i])) i++;
                tokens.Add(input.Substring(start, i - start));
                continue;
            }
            if ("+-*/^()".Contains(c))
            {
                tokens.Add(c.ToString());
                i++;
                continue;
            }
            i++; // пропускаем неизвестные символы
        }
        return tokens;
    }
}
