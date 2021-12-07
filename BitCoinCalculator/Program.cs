using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitCoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRate currentBitCoin = GetRates();
            Console.WriteLine("Calculate in: EUR/USD/GBP:");
            string userChoice = Console.ReadLine();
            Console.WriteLine("Enter the amount of bitcoin");
            float userCoins = float.Parse(Console.ReadLine());
            float currentRate = 0;

            if(userChoice == "EUR")
            {
                currentRate = currentBitCoin.bpi.EUR.rate_float;
            }
            else if(userChoice == "USD")
            {
                currentRate = currentBitCoin.bpi.USD.rate_float;
            }
            else if(userChoice == "GBP")
            {
                currentRate = currentBitCoin.bpi.EUR.rate_float;
            }
            else
            {
                Console.WriteLine("Not valid");
            }
            float result = currentRate * userCoins;
            Console.WriteLine($"BTC value: {result} {userChoice}");
        }


        public static BitcoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitcoinRate bitcoinData;

            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoinData = JsonConvert.DeserializeObject<BitcoinRate>(response);
            }

            return bitcoinData;
        }
    }
}
