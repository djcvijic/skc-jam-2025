using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public static class Extensions
{
    public static T GetRandomElement<T>(this ICollection<T> collection)
    {
        var random = new Random();
        var index = random.Next(collection.Count);
        return collection.ElementAt(index);
    }
    
    [Serializable]
    public struct SerializedKeyValuePair<TKey, TValue>
    {
        [SerializeField] public TKey Key;
        [SerializeField] public TValue Value;

        public SerializedKeyValuePair(TKey type, TValue value)
        {
            Key = type;
            Value = value;
        }
    }
}