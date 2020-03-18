using System.Collections.Generic;
using Newtonsoft.Json;

namespace ReactWebApp.Models
{
    /// <summary>
    /// An object to report the to  possible test cases from the Algorithm Tester
    /// 1. If success output the remaining inventory
    /// 2. If out of stock outpt which product Refrostly ran out of, and when it ran out
    /// </summary>
    public class ResultValue
    {

        [JsonProperty("success")]
        public bool IsSuccess { get; set; }

        public Dictionary<string, int> Inventory { get; set; }

        public Order FailingOrder { get; set; }
    }
}