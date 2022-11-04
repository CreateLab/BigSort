using System.Collections.Concurrent;
using System.Diagnostics;
using BigSort.Comparators;

namespace BigSort;

public class BigSort
{
    public static async Task Run(string[] args)
    {
        var chunkSize = 10000;
        var st = new Stopwatch();
        st.Start();
        var files = await SeparateFileToChunk(args[0], chunkSize);
        st.Stop();
        Console.WriteLine($"SeparateFileToChunk: {st.Elapsed} ms");
        await MergeSort(files, new List<string>(), new LineComparator(), args[0]);
    }

    static async Task MergeSort(List<string> files, List<string> resultFiles, LineComparator lineComparator,
        string startFile)
    {
        var lisfOfTasks = new List<Task<string>>();
        for (var i = 0; i < files.Count; i += 2)
        {
            var file1 = files[i];
            var file2 = i + 1 < files.Count ? files[i + 1] : null;
            var task = Task<string>.Run(() => MergeWithSort(file1, file2, lineComparator));


            lisfOfTasks.Add(task);
        }

        var newFiles = await Task.WhenAll(lisfOfTasks);
        resultFiles.AddRange(newFiles);

        if (resultFiles.Count == 1)
        {
            File.Replace(resultFiles[0], startFile, null);
        }
        else
        {
            files.Clear();
            await MergeSort(resultFiles, files, lineComparator, startFile);
        }
    }

    static string MergeWithSort(string filePath, string? secondFilePath, LineComparator lineComparator)
    {
        if (secondFilePath == null)
        {
            return filePath;
        }

        var resultFile = Guid.NewGuid().ToString() + ".txt";
        using (var file1 = new StreamReader(filePath))
        {
            using (var file2 = new StreamReader(secondFilePath))
            {
                using var result = new StreamWriter(resultFile);

                var line1 = file1.ReadLine();
                var line2 = file2.ReadLine();

                while (!(string.IsNullOrEmpty(line1) && string.IsNullOrEmpty(line2)))
                {
                    if (line1 == null)
                    {
                        result.WriteLine(line2);
                        line2 = file2?.ReadLine();
                    }
                    else if (line2 == null)
                    {
                        result.WriteLine(line1);
                        line1 = file1.ReadLine();
                    }
                    else
                    {
                        var compare = lineComparator.Compare(line1, line2);
                        if (compare < 0)
                        {
                            result.WriteLine(line1);
                            line1 = file1.ReadLine();
                        }
                        else
                        {
                            result.WriteLine(line2);
                            line2 = file2?.ReadLine();
                        }
                    }
                }
            }
        }

        File.Delete(filePath);
        File.Delete(secondFilePath);

        return resultFile;
    }

    static async Task<List<string>> SeparateFileToChunk(string fileName, int chunkSize)
    {
        var chunkNumber = 0;
        var tasks = new List<Task>();
        var files = new ConcurrentBag<string>();

        using var fileStream = new FileStream(fileName, FileMode.Open);
        using var reader = new StreamReader(fileStream);
        while (!reader.EndOfStream)
        {
            var i = 0;
            var listOfChunk = new List<string>(chunkSize);
            while (i < chunkSize && !reader.EndOfStream)
            {
                var line = reader.ReadLine();
                listOfChunk.Add(line);
                i++;
            }

            var task = Task.Run(() => ChunkNumber(listOfChunk, chunkNumber, files));
            tasks.Add(task);
        }

        await Task.WhenAll(tasks.ToArray());


        return files.ToList();
    }

    private static void ChunkNumber(List<string> listOfChunk, int chunkNumber, ConcurrentBag<string> files)
    {
        var orderedEnumerable = listOfChunk.OrderBy(x => x, new LineComparator());
        var fileChunkName = Guid.NewGuid() + ".txt";
        File.WriteAllLines(fileChunkName, orderedEnumerable);
        files.Add(fileChunkName);
    }
}