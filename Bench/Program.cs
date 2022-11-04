// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

var sum = BenchmarkRunner.Run<Bench.Bench>();