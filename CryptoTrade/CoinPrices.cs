using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using CryptoTrade.Models.CoinModels;

namespace CryptoTrade
{
    class CoinPrices
    {
        

        
        public static List<CoinPriceNice> GetAllCoinPrices()
        {
            const string BINANCE_BULLSHIT_FAKE_COIN = "123456";

            //gets the api data from the url
            
            string data = Helpers.GetDataFromUrl("https://api.binance.com/api/v1/ticker/allPrices");

            //jsonconvert.deserializeobject<T> automatically parses json into the object T which is a list of coinpriceraw objects
            //coinpriceraw is the format they have in the api
            List<CoinPriceRaw> coinPricesRaw = JsonConvert.DeserializeObject<List<CoinPriceRaw>>(data);
            //coinpricenice has the base and actual coin symbols separated, we can add all the other requisite shit there too
            List<CoinPriceNice> coinPrices = new List<CoinPriceNice>();

            //reads each raw coin into a nice form
            foreach (CoinPriceRaw rawCoin in coinPricesRaw)
            {
                if (rawCoin.symbol.Equals(BINANCE_BULLSHIT_FAKE_COIN))
                    continue;
                CoinPriceNice coin = new CoinPriceNice();
                //remove the base currency from the end of the symbol
                string baseSymbol = GetBaseCurrency(rawCoin.symbol);
                coin.baseSymbol = baseSymbol;
                //cuts off the base currency symbol from the end of the raw symbol, which is something like XRPBTC
                coin.coinSymbol = rawCoin.symbol.Substring(0, rawCoin.symbol.Length - baseSymbol.Length);
                coin.price = rawCoin.price;
                coinPrices.Add(coin);
            }
            return coinPrices;
        }
        private static string GetBaseCurrency(string symbol)
        {
            //this is the only hardcoded thing we need, if we know all the base currencies then we good homie
            List<string> baseCurrencies = new List<string> { "BNB", "BTC", "ETH", "USDT" };
            foreach (string baseCurrency in baseCurrencies)
            {
                //takes the last x characters of the symbol, which is like XRPBTC where x is the length of the current basecurrency we're testing
                string endOfSymbol = symbol.Substring(symbol.Length - baseCurrency.Length);
                //if that end of the symbol is equal to our basecurrency, then we've found what we need
                if (baseCurrency.Equals(endOfSymbol))
                {
                    return baseCurrency;
                }
            }
            //should never be hit, only was hit because binance has a coin with the symbol 123456 fuck you binance
            return "shit's fucked yo";

        }

    }

    
}
