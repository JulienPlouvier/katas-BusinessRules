namespace BusinessRules.Infrastructure.SyntaxicSugar;
using System;
using System.Collections.Generic;

public static class EnumerableExtensions
{
    public static IEnumerable<T> AddOrReplace<T>(this IEnumerable<T> list, T item, Func<T, bool> unicitySelector)
    {
        bool hasReplaced = false;
        foreach (var elem in list)
        {
            if (unicitySelector(elem))
            {
                hasReplaced = true;
                yield return item;
            }
            else
                yield return elem;
        }
        if (!hasReplaced)
            yield return item;
    }
}
