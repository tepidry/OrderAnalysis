using System;
using Newtonsoft.Json;

namespace ReactWebApp.Models
{
    public class Order
    {
        [JsonProperty("customer_id")]
        public long CustomerId { get; set; }

        [JsonProperty("order_id")]
        public long OrderId { get; }

        [JsonProperty("order_date")]
        public DateTime OrderDate { get; set; }

        [JsonProperty("item_ordered")]
        public string ItemOrdered { get; set; }

        [JsonProperty("item_quantity")]
        public int ItemQuantity { get; set; }

        [JsonProperty("item_price")]
        public decimal ChargePrice { get; set; }
    }
}