using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CryptoTrade
{
    class MarketDepth
    {
        const int PRICE_POSITION_FROM_DEPTH_API = 0;
        const int QUANTITY_POSITION_FROM_DEPTH_API = 1;

        //coin depth from binance api, its not that pretty
        class CoinDepthRaw
        {
            public int lastUpdateId { get; set; }
            public List<List<object>> bids { get; set; }
            public List<List<object>> asks { get; set; }
            public CoinDepthRaw(int lastUpdateId, List<List<object>> bids, List<List<object>> asks)
            {
                this.lastUpdateId = lastUpdateId;
                this.bids = bids;
                this.asks = asks;
            }


        }
        //very unsure what to name this class xd
        public class QuantityPrice
        {
            public decimal quantity { get; set; }
            public decimal price { get; set; }
            public QuantityPrice(string p, string q)
            {
                this.quantity = Convert.ToDecimal(q);
                this.price = Convert.ToDecimal(p);
            }
        }

        public class CoinDepth
        {
            public int lastUpdateId { get; set; }
            public DateTime time { get; set; }
            public List<QuantityPrice> bids { get; set; }
            public List<QuantityPrice> asks { get; set; }

        }
        //bids and asks come in the same format of a list of 3 length lists, where the inner list is [price,amount,[]] idk why it always ends with an empty list
        private static List<QuantityPrice> ReadBidsOrAsks(List<List<object>> prices)
        {
            List<QuantityPrice> quantityPrices = new List<QuantityPrice>();
            foreach (List<object> o in prices)
            {
                //casts the elements of the generic object to decimals, object[0] is the price, object[1] is the amount ordered at that price
                QuantityPrice qp = new QuantityPrice(o[PRICE_POSITION_FROM_DEPTH_API].ToString(), o[QUANTITY_POSITION_FROM_DEPTH_API].ToString());
                quantityPrices.Add(qp);
            }
            return quantityPrices;
        }
        //it looks like their api's only give 100 bids and 100 asks, so we might not be getting full market depth
        public static CoinDepth GetCoinDepth(string symbol)
        {
            string url = "https://api.binance.com/api/v1/depth?symbol=" + symbol.ToUpper();
            string data = Helpers.GetDataFromUrl(url);
            CoinDepthRaw coinDepthRaw = JsonConvert.DeserializeObject<CoinDepthRaw>(data);
            CoinDepth coinDepth = new CoinDepth();
            coinDepth.lastUpdateId = coinDepthRaw.lastUpdateId;
            coinDepth.time = DateTime.Now;
            coinDepth.bids = ReadBidsOrAsks(coinDepthRaw.bids);
            coinDepth.asks = ReadBidsOrAsks(coinDepthRaw.asks);
            return coinDepth;

        }
    }
}
