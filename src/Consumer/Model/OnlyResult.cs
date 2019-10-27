using System;
using System.Collections.Generic;
using System.Text;

namespace Consumer.Model
{

    public class OnlyResult
    {
        public Result Symbol { get; set; }
    }

    public class Result
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
}
