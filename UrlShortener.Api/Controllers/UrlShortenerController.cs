using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Business;
using UrlShortener.Data.Models;

namespace UrlShortener.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly ILogger<UrlShortenerController> _logger;
       
        UrlManager urlManager = new UrlManager();

        public UrlShortenerController(ILogger<UrlShortenerController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getOriginalURL/{shortUrlKey}", Name = "getOriginalURL")]
        public ActionResult Get(string shortUrlKey)
        {
           string originalURL=urlManager.GetOriginalURL(shortUrlKey);
            if (originalURL!=null)
            {
                var res = new { key = shortUrlKey, originalUrl = $"{(Request.IsHttps ? "https://" : "http://")}{originalURL}" };
                return Ok(res);
            }
            else
            {
                return Ok("NoData");
            }
           
        }

        [HttpPost("shorten", Name = "shorten")]
        public ActionResult Post([FromBody] ShortenRequestModel model)
        {
            string shortURL=urlManager.ShortenURL(model.url, model.customUrl);
            var res = new { key = shortURL, ShortUrl = $"{(Request.IsHttps ? "https://" : "http://")}{ Request.Host.Value}/{shortURL}" };
            return Ok(res);
        }
    }
}
