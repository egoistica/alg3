
internal class Program
{
    public static void Main(string[] args)
    {
        var fb = new Benchmark();
        var sb = new BenchmarkPostfix();
        
        fb.Run();
        sb.Run();
    }
    
}
