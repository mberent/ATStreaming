using ATStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using ATStreaming.Streams.Inputs.Interfaces;

namespace ATStreaming.Streams.Inputs
{
    public class StockQuotesStream : IInputStream<StockQuote>
    {
        public string Company { get; private set; }
        public string Market { get; private set; }
        public IObservable<StockQuote> Inputs
        {
            get
            {
                return _stockQuotesSubcject.AsObservable();
            }
        }

        private string _inputFilePath;
        private Subject<StockQuote> _stockQuotesSubcject;

        public StockQuotesStream(string company, string market, string inputFilePath)
        {
            Company = company;
            Market = market;

            _inputFilePath = inputFilePath;
            _stockQuotesSubcject = new Subject<StockQuote>();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
