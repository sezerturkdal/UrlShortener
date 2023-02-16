using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShortener.Data.Models
{
    public class ShortenRequestModel
    {
        public string url { get; set; }
        public string customUrl { get; set; }
    }
}
