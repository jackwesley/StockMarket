using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Producer.Model
{
    public class SellingRate
    {
        public string by { get; set; }
        public bool valid_key { get; set; }
        public Results results { get; set; }
        public double execution_time { get; set; }
        public bool from_cache { get; set; }
    }

    public class MarketTime
    {
        public string open { get; set; }
        public string close { get; set; }
        public int timezone { get; set; }
    }

    public class Symbol
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        public string currency { get; set; }
        public MarketTime market_time { get; set; }
        public double market_cap { get; set; }
        public double price { get; set; }
        public double change_percent { get; set; }
        public string updated_at { get; set; }
    }

    public class Results
    {
        public Symbol Symbol { get; set; }
    }
}
