using BigSort.Extensions;

namespace BigSort.Comparators;

public class LineComparator : IComparer<string>
{
    /// <inheritdoc />
    public int Compare(string? x, string? y)
    {
        var xdot = x.IndexOf('.');
        var ydot = y.IndexOf('.');

        var lineResult = x.CompareRangeTo(y,xdot,ydot);
        if(lineResult != 0)
            return lineResult;
        return  x.CompareRangeBeforeTo(y,xdot,ydot);;
    }
}

