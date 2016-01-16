using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATStreaming.Models
{
    public class StockQuoteSignal
    {
        public StockQuote StockQuote { get; set; }
        public bool Signal { get; set; }
    }
}
