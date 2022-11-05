using System.Collections.Concurrent;
using BigSort.Comparators;

namespace BigSort.Model;

public class TaskParam
{
    public string InputFirstFile { get; set; }
    public string InputSecondFile { get; set; }
    public LineComparator LineComparator { get; set; }
    public ConcurrentBag<string> OutputFiles { get; set; }
    public CountdownEvent CountdownEvent { get; set; }
}