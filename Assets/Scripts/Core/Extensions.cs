using System.Collections.Generic;

namespace CountMasters.Core
{
    public static class Extensions
    {
        public static void AddSafe<TK, TV>(this Dictionary<TK, TV> dictionary, TK key, TV value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
    }
}