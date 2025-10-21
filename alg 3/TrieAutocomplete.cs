// Пример применения Дерева: Автокомплит с префиксным деревом (Trie)
// Применение: автодополнение в поисковых системах, IDE, мобильных клавиатурах
public class TrieNode
{
    public Dictionary<char, TrieNode> Children { get; set; }
    public bool IsEndOfWord { get; set; }
    public string Word { get; set; }

    public TrieNode()
    {
        Children = new Dictionary<char, TrieNode>();
        IsEndOfWord = false;
        Word = "";
    }
}

public class TrieAutocomplete
{
    private TrieNode _root;

    public TrieAutocomplete()
    {
        _root = new TrieNode();
    }

    public void Insert(string word)
    {
        var current = _root;
        
        foreach (char c in word.ToLower())
        {
            if (!current.Children.ContainsKey(c))
            {
                current.Children[c] = new TrieNode();
            }
            current = current.Children[c];
        }
        
        current.IsEndOfWord = true;
        current.Word = word;
    }

    public List<string> Search(string prefix)
    {
        var results = new List<string>();
        var current = _root;
        
        // Находим узел, соответствующий префиксу
        foreach (char c in prefix.ToLower())
        {
            if (!current.Children.ContainsKey(c))
            {
                return results; // Префикс не найден
            }
            current = current.Children[c];
        }
        
        // Собираем все слова, начинающиеся с этого префикса
        CollectWords(current, results);
        
        return results;
    }

    private void CollectWords(TrieNode node, List<string> results)
    {
        if (node.IsEndOfWord)
        {
            results.Add(node.Word);
        }
        
        foreach (var child in node.Children.Values)
        {
            CollectWords(child, results);
        }
    }

    public bool Contains(string word)
    {
        var current = _root;
        
        foreach (char c in word.ToLower())
        {
            if (!current.Children.ContainsKey(c))
            {
                return false;
            }
            current = current.Children[c];
        }
        
        return current.IsEndOfWord;
    }

    public void PrintTrie()
    {
        Console.WriteLine("Структура Trie:");
        PrintNode(_root, "", 0);
    }

    private void PrintNode(TrieNode node, string prefix, int depth)
    {
        if (node.IsEndOfWord)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}'{prefix}' (слово)");
        }
        
        foreach (var kvp in node.Children)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}'{kvp.Key}'");
            PrintNode(kvp.Value, prefix + kvp.Key, depth + 1);
        }
    }

    public static void Demonstrate()
    {
        var trie = new TrieAutocomplete();
        
        // Добавляем словарь
        string[] words = {
            "algorithm", "array", "binary", "cache", "data", "database",
            "function", "graph", "hash", "heap", "list", "queue", "stack",
            "tree", "sort", "search", "insert", "delete", "update"
        };
        
        foreach (string word in words)
        {
            trie.Insert(word);
        }

        Console.WriteLine("=== Демонстрация автокомплита ===");
        
        string[] prefixes = { "al", "da", "st", "tr", "qu", "xyz" };
        
        foreach (string prefix in prefixes)
        {
            var suggestions = trie.Search(prefix);
            Console.WriteLine($"Префикс '{prefix}': {string.Join(", ", suggestions)}");
        }
        
        Console.WriteLine("\nПроверка существования слов:");
        string[] testWords = { "algorithm", "array", "nonexistent", "stack" };
        foreach (string word in testWords)
        {
            Console.WriteLine($"'{word}': {(trie.Contains(word) ? "найдено" : "не найдено")}");
        }
    }
}
