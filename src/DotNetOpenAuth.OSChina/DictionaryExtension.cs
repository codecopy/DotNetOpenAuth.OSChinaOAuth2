using System;
using System.Collections.Generic;

namespace DotNetOpenAuth
{
    public static class DictionaryExtension
    {
        public static void AddItemIfNotEmpty(this Dictionary<string, string> dictionary, string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            if (!string.IsNullOrEmpty(value))
            {
                dictionary[key] = value;
            }
        }
    }
}