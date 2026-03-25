using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DesktopFilesGui.Extensions;

public static class DictionaryExtensions
{
    public static bool ContainsRangeKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<TKey> keys)
    {
        return keys
            .Select(dictionary.ContainsKey)
            .All(result => result);
    }
}