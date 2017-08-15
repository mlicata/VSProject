using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;



namespace JsonDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<string> portfolioSymbols = new List<string>();
            portfolioSymbols = getSymbols($"portfolioSymbols");

            List<string> comparisonSymbols = new List<string>();
            comparisonSymbols = getSymbols($"comparisonSymbols");




            Console.ReadKey();




            //////TBD: STORE SYMBOLS AND VALUES IN CONFIG


            //List<Quote> myPortfolioData = new List<Quote>();
            //List<Quote> comparisonData = new List<Quote>();

            //myPortfolioData = loadQuoteData(portfolioSymbols);
            ////comparisonData = loadQuoteData(portfolioSymbols);

            //return Ok(myPortfolioData.Count);

            //List <string> comarisonSymbols = new List<string>();


        }

        private static List<string> getSymbols(string str)
        {
            List<string> temp = new List<string>();
            string filename = $"C:\\Users\\malicata\\Documents\\Visual Studio 2017\\Projects\\StockManager.app\\StockManager\\appsettings.json";
            StreamReader file = File.OpenText(filename);
            string jsonStr = file.ReadToEnd();
            dynamic jsonObject = JsonConvert.DeserializeObject(jsonStr);
            foreach (var obj in jsonObject[str])
            {
                temp.Add((string)obj.Value);
            }
            return temp;
        }

        
        private List<Quote> loadQuoteData(List<string> symbols)
        {
            List<Quote> quotesToReturn = new List<Quote>();
            string key = Configuration["vantageAPIKey:key"];
            foreach (var symbol in symbols)
            {
                string result = GetHistoricalQuote(symbol, key);
                if (result != null)
                {
                    Quote qte = new Quote();
                    dynamic jsonObject = JsonConvert.DeserializeObject(result);
                    qte.symbol = jsonObject["Meta Data"]["2. Symbol"];
                    foreach (dynamic obj in jsonObject["Time Series (Daily)"].Children())
                    {
                        Price tempPrice = new Price();
                        tempPrice.date = (DateTime)obj.Name;
                        tempPrice.price = obj.First["4. close"];
                        qte.Prices.Add(tempPrice);
                    }
                    quotesToReturn.Add(qte);
                }
            }
            return quotesToReturn;
        }
        

        public string GetHistoricalQuote(string symbol, string key)
        {
            try
            {
                string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&apikey={key}&symbol={symbol}";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("").Result;
                return response.Content.ReadAsStringAsync().Result
            }
            catch (Exception e)
            {
                //_logger.LogWarning($"Quote retrieveal failed for {symbol}");
                return null;
            }
        }

    }
}
