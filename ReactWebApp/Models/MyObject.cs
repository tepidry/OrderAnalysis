using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReactWebApp.Models
{
    public class MyObject
    {
        [JsonProperty("orders")]
        public IEnumerable<Order> Orders { get; set; }

        [JsonProperty("restocks")]
        public IEnumerable<Restock> Restocks { get; set; }
    }
}