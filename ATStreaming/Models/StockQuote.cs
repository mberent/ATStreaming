using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATStreaming.Models
{
    public class StockQuote
    {
        public string Company { get; private set; }
        public string Market { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Open { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }
        public decimal Close { get; private set; }
        public long Volume { get; private set; }
        public decimal Turnover { get; private set; }

        public StockQuote(
            string company, string market, DateTime date, 
            decimal open, decimal high, decimal low, 
            decimal close, long volume, decimal turnover)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
            Turnover = turnover;
        }
    }
}
