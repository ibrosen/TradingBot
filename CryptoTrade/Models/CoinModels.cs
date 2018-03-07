using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoTrade.Models.CoinModels
{
    //coin price but with separated coin symbol and base symbol, so for XRPBTC XRP is the coin symbol and BTC is the base symbol
    public class CoinPriceNice
    {

        public decimal price { get; set; }
        public string coinSymbol { get; set; }
        public string baseSymbol { get; set; }
        
    }
    //from the api, it returns something called symbol which looks like  XRPBTC or BTCUSDT or something
    public class CoinPriceRaw
    {

        public string symbol { get; set; }
        public decimal price { get; set; }

        //turns out to use JsonConvert.DeserializeObject<T> to turn json into whatever custom object/list of custom objects you want
        //you need to write a constructor or it'll be empty, PSA homies
        public CoinPriceRaw(string symbol, decimal price)
        {
            this.symbol = symbol;
            this.price = price;
        }
    }
    
}
