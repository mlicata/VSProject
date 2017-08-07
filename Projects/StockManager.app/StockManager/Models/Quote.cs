using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManager.Models
{
    public class Quote
    {
        public string symbol { get; set; }
        public ICollection<Price> Prices { get; set; }
    }
}
