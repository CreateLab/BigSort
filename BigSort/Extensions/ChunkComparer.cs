namespace BigSort.Extensions;

public static class ChunkComparer
{
    public static int CompareRangeTo(this string current, string target, int positionX, int positionY)
    {
        var length = Math.Min(current.Length - positionX, target.Length - positionY);
        for (var i = 0; i < length; i++)
        {
            var result = current[positionX + i].CompareTo(target[positionY + i]);
            if (result != 0)
            {
                return result;
            }
        }

        return current.Length - target.Length;
    }

    public static int CompareRangeBeforeTo(this string current, string target, int positionX, int positionY)
    {
        var diff = positionX - positionY;
        if (diff != 0)
        {
            return diff;
        }

        var length = Math.Min(positionX, positionY);
        
        for (var i = 0; i < length; i++)
        {
            var result = (current[i] - '0').CompareTo((target[i] - '0'));
            if (result != 0)
            {
                return result;
            }
        }

        return 0;
    }
}