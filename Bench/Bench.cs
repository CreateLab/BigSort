using BenchmarkDotNet.Attributes;
using Generator;

namespace Bench;

public class Bench
{
    public string Path { get; set; } = @"C:\Users\lary\OneDrive\Рабочий стол\Новая папка\test.txt";
    public Bench()
    {
        Gen.Run(new []{Path,"1000"});
    }

    [Benchmark]
    public void Test() => BigSort.BigSort.Run(new[] { Path });
}