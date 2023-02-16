using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Text;

namespace UrlShortener.Data
{
    public class UrlModel
    {
        public long Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; } // currenly key
        public string CreatedDate { get; set; }
    }

    [Table("URL")] // SQLite attribute
    [DataContract(Name = "URL", Namespace = "")]
    public sealed class URL
    {
        /// <summary>
        /// Unique Id of the codebook.
        /// </summary>
        [Column("Id")] // SQLite attribute3
        [NotNull]
        [IgnoreDataMember]
        public int Id { get; set; }

        [Column("OriginalUrl")] // SQLite attribute
        [NotNull] // SQLite attribute
        public string OriginalUrl { get; set; }

        [Column("ShortUrl")] // SQLite attribute
        [NotNull] // SQLite attribute
        public string ShortUrl { get; set; }

        [Column("CreatedDate")] // SQLite attribute
        [NotNull] // SQLite attribute
        public string CreatedDate { get; set; }

        public URL()
        {
        }
    }
}
