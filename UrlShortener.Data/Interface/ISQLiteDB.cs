using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShortener.Data.Interface
{
    interface ISQLiteDB
    {
        public bool SaveURL(string originalUrl, string shortUrl);
        public string GetOriginalURL(string url);
    }
}
