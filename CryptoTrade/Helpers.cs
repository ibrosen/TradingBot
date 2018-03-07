using System.Net;
namespace CryptoTrade
{
    class Helpers
    {
        public static string GetDataFromUrl(string url)
        {
            var client = new WebClient();
            return client.DownloadString(url);
        }
    }
}
