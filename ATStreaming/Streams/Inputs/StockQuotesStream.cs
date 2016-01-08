using ATStreaming.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using ATStreaming.Streams.Inputs.Interfaces;
using ATStreaming.Models.Services.Interfaces;
using log4net;

namespace ATStreaming.Streams.Inputs
{
    public class StockQuotesStream : IInputStream<StockQuote>
    {
        private static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IObservable<StockQuote> Inputs
        {
            get
            {
                return _stockQuotesSubcject.AsObservable();
            }
        }

        private ISourceReader<StockQuote> _stockQuotesReader;
        private Subject<StockQuote> _stockQuotesSubcject = new Subject<StockQuote>();

        public StockQuotesStream(ISourceReader<StockQuote> stockQuotesReader)
        {
            _stockQuotesReader = stockQuotesReader;
        }

        public void Start(SourceDescriptor descriptor)
        {
            try
            {
                foreach (var stockQuote in _stockQuotesReader.Read(descriptor))
                {
                    _stockQuotesSubcject.OnNext(stockQuote);
                }
                _stockQuotesSubcject.OnCompleted();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _stockQuotesSubcject.OnError(ex);
            }
        }
    }
}
