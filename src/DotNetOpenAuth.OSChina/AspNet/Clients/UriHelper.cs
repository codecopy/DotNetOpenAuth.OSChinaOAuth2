using System;
using System.Collections.Specialized;

namespace DotNetOpenAuth.AspNet.Clients
{
    public static class UriHelper
    {
        public static string NormalizeHexEncoding(this string url)
        {
            var array = url.ToCharArray();
            for (var i = 0; i < array.Length - 2; i++)
            {
                if (array[i] != '%') continue;

                array[i + 1] = Char.ToUpperInvariant(array[i + 1]);
                array[i + 2] = Char.ToUpperInvariant(array[i + 2]);
                i += 2;
            }
            return new string(array);
        }

        public static string GetProviderNameFromQueryString(NameValueCollection queryString)
        {
            var result = queryString["__provider__"];
            //commented out stuff
            return result;
        }
    }
}