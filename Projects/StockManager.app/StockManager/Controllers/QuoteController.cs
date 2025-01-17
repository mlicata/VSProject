﻿using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using StockManager.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;


namespace StockManager.Controllers
{
    public class QuoteController : Controller
    {
        private ILogger<QuoteController> _logger;
        public IConfigurationRoot Configuration { get; }


        public QuoteController(ILogger<QuoteController> logger, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            _logger = logger;
        }


        [Route("api/testquote")]
        public IActionResult GetTestQuote()
        {
            List<string> portfolioSymbols = new List<string>();
            List<string> comparisonSymbols = new List<string>();
            List<Quote> myPortfolioData = new List<Quote>();
            List<Quote> comparisonData = new List<Quote>();

            portfolioSymbols = getSymbols($"portfolioSymbols");

            comparisonSymbols = getSymbols($"comparisonSymbols");

            myPortfolioData = loadQuoteData(portfolioSymbols);
            comparisonData = loadQuoteData(portfolioSymbols);

            return Ok(myPortfolioData.Count + " " + comparisonData.Count);
        }

        private static List<string> getSymbols(string str)
        {
            List<string> temp = new List<string>();
            string filename = $"appsettings.json";
            StreamReader file = System.IO.File.OpenText(filename);
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
                if(result != null)
                {
                    Quote qte = new Quote();
                    dynamic jsonObject = JsonConvert.DeserializeObject(result);
                    qte.symbol = jsonObject["Meta Data"]["2. Symbol"];
                    int counter = 1;
                    foreach (dynamic obj in jsonObject["Time Series (Daily)"].Children())
                    {
                        Price tempPrice = new Price();
                        tempPrice.date = (DateTime)obj.Name;
                        tempPrice.price = obj.First["4. close"];
                        qte.Prices.Add(tempPrice);
                        if(counter >=30 )
                        {
                            break;
                        }else{
                            counter++;
                        }
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
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Quote retrieveal failed for {symbol}");
                return null;
            }
        }
    }
}