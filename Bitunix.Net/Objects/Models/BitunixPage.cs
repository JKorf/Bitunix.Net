using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bitunix.Net.Objects.Models
{
    /// <summary>
    /// Page
    /// </summary>
    public record BitunixPage<T>
    {
        /// <summary>
        /// ["<c>total</c>"] Total data set
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>pageNum</c>"] Page
        /// </summary>
        [JsonPropertyName("pageNum")]
        public int Page { get; set; }
        /// <summary>
        /// ["<c>pageSize</c>"] Page size
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public T[] Data { get; set; } = [];
    }
}
