namespace BigSort.Comparators;

public class LineComparator:IComparer<string>
{
    /// <inheritdoc />
    public int Compare(string? x, string? y)
    {
        var xSplit = x?.Split('.',2);
        var ySplit = y?.Split('.',2);
        if(xSplit[0] != ySplit[0])
        {
            var r = string.Compare(xSplit[1], ySplit[1]);
            return r;
        }

        return int.Parse(xSplit[0]).CompareTo(int.Parse(ySplit[0]));
    }
}