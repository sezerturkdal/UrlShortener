using System;
using System.IO;
using UrlShortener.Data;

namespace UrlShortener.Business
{
    public class UrlManager
    {
        private SQLiteDB _db;

        public UrlManager()
        {
            _db = new SQLiteDB();
        }

        public string ShortenURL(string originalUrl, string customUrl)
        {
            string shortenedUrl = "";
            try
            {
                var url = new Uri(originalUrl);
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("Invalid URL", ex); 
            }
            
            if (customUrl!=null)
            {
                if (CheckIsExists(customUrl))
                    throw new  Exception("The name is already exists");
                else
                {
                    shortenedUrl = customUrl;
                }
            }
            else
            {
               shortenedUrl = Guid.NewGuid().ToString("N").Substring(0, 6).ToLower();
            }
            
            _db.SaveURL(originalUrl, shortenedUrl);

            return shortenedUrl;
        }

        public string GetOriginalURL(string shortUrl)
        {
            return _db.GetOriginalURL(shortUrl);
        }

        public bool CheckIsExists(string customUrl)
        {
            return _db.CheckIsExists(customUrl);
        }
    }
}
