using System.Diagnostics;
using Generator;

string path = @"C:\Users\lary\OneDrive\Рабочий стол\Новая папка\test.txt";


Gen.Run(new[] { path, "1000" });

var stopWatch = new Stopwatch();
stopWatch.Start();
await BigSort.BigSort.Run(new[] { path });
stopWatch.Stop();
Console.WriteLine(stopWatch.Elapsed);