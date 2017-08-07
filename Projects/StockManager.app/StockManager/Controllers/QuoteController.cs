using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StockManager.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.JsonPatch;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc.Formatters.Json;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace StockManager.Controllers
{
    public class QuoteController : Controller
    {
        public HttpWebRequest request;
        public HttpWebResponse response;
        public Stream streamResponse;

        private ILogger<QuoteController> _logger;

        public QuoteController(ILogger<QuoteController> logger)
        {
            _logger = logger;
        }


        [Route("api/testquote")]
        public IActionResult GetTestQuote()
        {
            List<string> portfolioSymbols = new List<string>();
            portfolioSymbols.Add("MSFT");
            portfolioSymbols.Add("EXPE");

            List<string> comarisonSymbols = new List<string>();
            comarisonSymbols.Add(".INX");
            comarisonSymbols.Add(".DJI");

            List<Quote> myPortfolioData = new List<Quote>();
            List<Quote> comparisonData = new List<Quote>();

            myPortfolioData = loadQuoteData(portfolioSymbols);
            //comparisonData = loadQuoteData(portfolioSymbols);

            return Ok(myPortfolioData.First().symbol);
        }

        private List<Quote> loadQuoteData(List<string> symbols)
        {
            List<Quote> quotesToReturn = new List<Quote>();
            foreach (var symbol in symbols)
            {
                JsonResult result = new JsonResult(GetHistoricalQuote(symbol));
                if(result != null)
                {
                    Quote qte = new Quote();
                    //dynamic jsonObject = JsonConvert.DeserializeObject(result.ToString().Trim());
                    qte.symbol = result.Value.ToString();
                    quotesToReturn.Add(qte);
                }
            }
            return quotesToReturn;
        }

        public JsonResult GetHistoricalQuote(string symbol)
        {
            try
            {
                string url = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&apikey=D5DH58NN7OUXAB53&symbol={symbol}";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("").Result;
                return new JsonResult(response.Content.ReadAsStreamAsync().Result);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Quote retrieveal failed for {symbol}");
                return null;
            }
        }
    }
}
