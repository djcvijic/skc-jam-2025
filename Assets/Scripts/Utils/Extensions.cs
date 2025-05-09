using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    public static T GetRandomElement<T>(this ICollection<T> collection)
    {
        var random = new Random();
        var index = random.Next(collection.Count);
        return collection.ElementAt(index);
    }
}