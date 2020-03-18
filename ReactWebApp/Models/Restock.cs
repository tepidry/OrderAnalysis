using System;
using Newtonsoft.Json;

namespace ReactWebApp.Models
{
    public class Restock
    {
        [JsonProperty("restock_date")]
        public DateTime DateOfRestock { get; set; }

        [JsonProperty("item_stocked")]
        public string ItemStocked { get; set; }

        [JsonProperty("item_quantity")]
        public int ItemQuantity { get; set; }

        [JsonProperty("manufacturer")]
        public string ManufacturerName { get; set; }

        [JsonProperty("wholesale_price")]
        public decimal PaidPrice { get; set; }
    }
}