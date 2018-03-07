using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using CrypthoesTestConsoleApp.Models.CoinModels;
namespace CryptoTrade
{
    class Program
    {
        static void Main(string[] args)
        {

            
            var coinPrices = CoinPrices.GetAllCoinPrices();
            //uses linq magic to get a list of all the coin symbols, then gets the distinct ones, just to see the num of unique coins on the market
             List<string> uniqueCoins = coinPrices.Select(c => c.coinSymbol).Distinct().ToList();


            var coinDepth = MarketDepth.GetCoinDepth("TRXETH");

        }


        

        
    }
    
    
}
