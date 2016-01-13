using ATStreaming.Models;
using ATStreaming.Models.Services;
using ATStreaming.Streams.Inputs;
using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using ATStreaming.Streams.Indexes;

namespace ATStreaming
{
    class Program
    {
        private static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            BasicConfigurator.Configure();

            var reader = new SourceReader();
            var inputSource = new StockQuotesStream(reader);
            var descriptor = new SourceDescriptor(@"Files\APPLE_NASDAQ.csv");

            inputSource.Inputs.Subscribe(
                next => _logger.InfoFormat("[{0}] {1} - {2}", next.Date.ToShortDateString(), next.Company, next.Close),
                error => _logger.Error(error),
                () => _logger.Info("Completed"));

            inputSource.Start(descriptor);

        }
    }
}
